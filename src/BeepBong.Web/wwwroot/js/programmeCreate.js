function updateLabel() {
	var input = document.getElementById('Programme_LogoUpload');
	
	if (input.files.length > 0)
	{
		document.getElementById('Programme_LogoUpload_Label').innerText = input.files[0].name;
	}
}