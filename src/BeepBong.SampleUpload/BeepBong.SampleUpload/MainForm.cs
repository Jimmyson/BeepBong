using System;
using Eto.Forms;
using Eto.Drawing;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace BeepBong.SampleUpload
{
	public partial class MainForm : Form
    {
        // Mutable Items
        private DataInteraction di;

        public MainForm(IConfig config)
		{
            di = new DataInteraction(config);

            InitializeComponent();

            // Bind Actions
            selectFileButton.Click += HandleFiles;
            selectFolderButton.Click += HandleFolder;
            scanFilesButton.Click += (sender, e) => ScanFilesAsync(sender, e).ConfigureAwait(false); ;
            uploadEntityButton.Click += (sender, e) => UploadFilesAsync(sender, e).ConfigureAwait(false);

            grid.DataStore = di.FileCollection;

            if (di.Config.IsConfigSetup())
            {
                di.GetTracklists();
                TracklistSelector.Enabled = true;
            }
            else
            {
                TracklistSelector.Enabled = false;
            }

            TracklistSelector.DataStore = di.Tracklists;

            // Event Binding
            clearListButton.Click += (sender, e) => di.FileCollection.Clear();
            TracklistSelector.DropDownClosed += (sender, e) =>
            {
                if (TracklistSelector.SelectedKey != null && TracklistSelector.SelectedKey != Guid.Empty.ToString())
                {
                    di.GetTracks(TracklistSelector.SelectedKey);
                    tracks.Text = string.Concat(di.Tracks.Select(t => t.Value + Environment.NewLine).ToList());

                    TrackSelector.DataStore = di.Tracks;
                }
            };

            di.FileCollection.CollectionChanged += (sender, e) =>
            {
                clearListButton.Enabled = (di.FileCollection.Count > 0);
                scanFilesButton.Enabled = (di.FileCollection.Count != 0);
                uploadEntityButton.Enabled = (di.FileCollection.Any(item => item.Scanned && item.Track != null));
            };

            grid.CellEdited += (sender, e) => 
                uploadEntityButton.Enabled = (di.FileCollection.Any(item => item.Scanned && item.Track != null));

            listReloadButton.Click += (sender, e) =>
            {
                if (di.Config.GetURL() != null)
                {
                    di.GetTracklists();
                    TracklistSelector.DataStore = di.Tracklists;
                    TracklistSelector.SelectedIndex = 0;
                    TracklistSelector.Enabled = true;
                }
                else
                {
                    TracklistSelector.Enabled = false;
                }
            };
        }

        /// <summary>
        /// Add files from the system to the File Collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleFiles(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.Ok)
            {
                di.AddFileToCollection(openFileDialog.Filenames);
            }
        }

        void HandleFolder(object sender, EventArgs e)
        {
            var dialog = new SelectFolderDialog();

            if (dialog.ShowDialog(this) == DialogResult.Ok)
            {
                var files = System.IO.Directory.GetFiles(dialog.Directory);
                ICollection<string> supportedFiles = new List<string>();

                foreach (var file in files)
                {
                    if (supportedExtensions.Any(file.EndsWith))
                    {
                        supportedFiles.Add(file);
                    }
                }

                di.AddFileToCollection(supportedFiles);
            }
        }

        /// <summary>
        /// Process audio files in the collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async Task ScanFilesAsync(object sender, EventArgs e)
        {
            grid.Enabled = false;

            foreach (Item i in di.FileCollection.Where(f => !f.Scanned).ToList())
            {
                di.ScanFile(i);
                grid.ReloadData(i.ID - 1);
            }

            grid.Enabled = true;
        }

        /// <summary>
        /// Process files in the collection to be sent to the cloud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async Task UploadFilesAsync(object sender, EventArgs e)
        {
            grid.Enabled = false;

            foreach (Item i in di.FileCollection.Where(f => !f.Uploaded && f.Scanned && f.Track != null && f.Sample != null).ToList())
            {
                di.UploadSample(i);
                grid.ReloadData(i.ID - 1);
            }

            grid.Enabled = true;
        }
    }
}
