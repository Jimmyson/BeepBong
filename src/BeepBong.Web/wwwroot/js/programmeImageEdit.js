function updateLabel() {
	var input = document.getElementById('Programme_ImageUpload');
	
	if (input.files.length > 0)
	{
		document.getElementById('Programme_ImageUpload_Label').innerText = input.files[0].name;
		document.getElementById("Programme_ImageIdChange").value = "";
	}

	document.getElementById("imageReset").disabled = false;
}

function clearImage() {
	document.getElementById('Programme_ImageUpload').value = "";
	document.getElementById('Programme_ImageUpload_Label').innerText = "Choose file...";
	document.getElementById('Programme_Image').style.display = "none";
	document.getElementById('Programme_ImageIdChange').value = "";

	document.getElementById("imageReset").disabled = false;
}

function resetImage() {
	document.getElementById('Programme_ImageUpload').value = "";
	document.getElementById('Programme_ImageUpload_Label').innerText = "Choose file...";
	document.getElementById('Programme_Image').removeAttribute("style");
	document.getElementById('Programme_ImageIdChange').value = document.getElementById('Programme_ImageId').value;

	document.getElementById("imageReset").disabled = true;
}