function updateLabel() {
	var input = document.getElementById('Broadcaster_ImageUpload');
	
	if (input.files.length > 0)
	{
		document.getElementById('Broadcaster_ImageUpload_Label').innerText = input.files[0].name;
	}
}

function clearImage() {
	document.getElementById('Broadcaster_ImageUpload').value = "";
	document.getElementById('Broadcaster_ImageUpload_Label').innerText = "Choose file...";
}