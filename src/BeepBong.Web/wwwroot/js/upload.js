var wavesurfer = WaveSurfer.create({
    container: '#waveform',
    waveColor: 'violet',
    progressColor: 'purple',
    splitChannels: true,
    forceDecode: true,
    plugins: [
        WaveSurfer.spectrogram.create({
            wavesurfer: wavesurfer,
            container: "#waveform-spec"
            //labels: true
        })
    ]
});

wavesurfer.on('play', function() {
    document.getElementById('play').style.display = "none";
    document.getElementById('pause').style.display = "inline";
});

wavesurfer.on('pause', function() {
    document.getElementById('play').style.display = "inline";
    document.getElementById('pause').style.display = "none";
});

// Load the WebAssembly MediaInfo module if the browser supports it,
// otherwise load the asmjs module
var MediaInfoJs = document.createElement('script');
if ('WebAssembly' in window) {
    MediaInfoJs.src = "../lib/mediainfojs/MediaInfoWasm.js";
} else {
    MediaInfoJs.src = "../lib/mediainfojs/MediaInfo.js";
}
document.body.appendChild(MediaInfoJs);

function addImages() {
	document.getElementById('Sample_WaveformImage').value = wavesurfer.exportImage();
	var canvasCount = document.getElementsByTagName('canvas').length;
	document.getElementById('Sample_SpecImage').value = document.getElementsByTagName('canvas')[canvasCount-1].toDataURL();
	document.getElementsByTagName('form')[0].submit();
}

// Continue initialization
MediaInfoJs.onload = function () {
    var MediaInfoModule, MI, processing = false, CHUNK_SIZE = 1024 * 1024;

    var finish = function() {
        MI.Close();
        MI.delete();
        processing = false;
    }

    // Examples about how to get results
    var showResult = function() {
        MI.Option('Inform'); // Reset custom output

        document.getElementById('Sample_SampleRate').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'SamplingRate');
        document.getElementById('Sample_SampleCount').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'SamplingCount');
        document.getElementById('Sample_Channels').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'Channel(s)');
        document.getElementById('Sample_BitRate').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'BitRate');
        document.getElementById('Sample_BitRateMode').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'BitRate_Mode');
        document.getElementById('Sample_BitDepth').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'BitDepth');

        document.getElementById('Sample_Codec').value = MI.Get(MediaInfoModule.Stream.General, 0, 'Format');

        if (MI.Get(MediaInfoModule.Stream.Audio, 0, 'Compression_Mode') != "")
            document.getElementById('Sample_Compression').value = MI.Get(MediaInfoModule.Stream.Audio, 0, 'Compression_Mode');
        else 
            document.getElementById('Sample_Compression').value = "None";

        var notes = "";

        if (MI.Get(MediaInfoModule.Stream.General, 0, 'Producer') != "")
            notes += "Producer: " + MI.Get(MediaInfoModule.Stream.General, 0, 'Producer') + '\r\n';
        if (MI.Get(MediaInfoModule.Stream.General, 0, 'Encoded_Date') != "")
            notes += 'Encoded Date: ' + MI.Get(MediaInfoModule.Stream.General, 0, 'Encoded_Date') + '\r\n';

        document.getElementById('Sample_Notes').value = notes.trim();
        
        finish();
    }

    var parseFile = function(file, callback) {
        if (processing) {
            return;
        }

        processing = true;

        var offset = 0;

        // Initialise MediaInfo
        MI = new MediaInfoModule.MediaInfo();

        //Open the file
        MI.Open(file, callback);
    }

    // Initialise emscripten module
    MediaInfoModule = MediaInfoLib({'postRun': function()
        {
            console.debug('MediaInfo ready');

            // Get selected file
            var input = document.getElementById('input');
            input.onchange = function()
            {
                if (input.files.length > 0)
                {
                    document.getElementById('input-label').innerText = input.files[0].name;
                    parseFile(input.files[0], showResult);
                    wavesurfer.loadBlob(input.files[0]);
                }
            }
        }
    });
};