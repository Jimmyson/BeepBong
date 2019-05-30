function updateLabel() {
	var input = document.getElementById('Programme_ImageUpload');
	
	if (input.files.length > 0)
	{
		document.getElementById('Programme_ImageUpload_Label').innerText = input.files[0].name;
	}
}

function clearImage() {
	document.getElementById('Programme_ImageUpload').value = "";
	document.getElementById('Programme_ImageUpload_Label').innerText = "Choose file...";
}