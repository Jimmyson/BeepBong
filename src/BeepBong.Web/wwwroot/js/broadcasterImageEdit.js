function updateLabel() {
	var input = document.getElementById('Broadcaster_ImageUpload');
	
	if (input.files.length > 0)
	{
		document.getElementById('Broadcaster_ImageUpload_Label').innerText = input.files[0].name;
		document.getElementById("Broadcaster_ImageIdChange").value = "";
	}

	document.getElementById("imageReset").disabled = false;
}

function clearImage() {
	document.getElementById('Broadcaster_ImageUpload').value = "";
	document.getElementById('Broadcaster_ImageUpload_Label').innerText = "Choose file...";
	document.getElementById('Broadcaster_Image').style.display = "none";
	document.getElementById('Broadcaster_ImageIdChange').value = "";

	document.getElementById("imageReset").disabled = false;
}

function resetImage() {
	document.getElementById('Broadcaster_ImageUpload').value = "";
	document.getElementById('Broadcaster_ImageUpload_Label').innerText = "Choose file...";
	document.getElementById('Broadcaster_Image').removeAttribute("style");
	document.getElementById('Broadcaster_ImageIdChange').value = document.getElementById('Broadcaster_ImageId').value;

	document.getElementById("imageReset").disabled = true;
}