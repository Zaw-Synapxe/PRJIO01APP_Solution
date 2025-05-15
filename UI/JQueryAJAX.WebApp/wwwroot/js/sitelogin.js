// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
	//console.log("Hello World!");

});

function LoginForm() {
	alert("Login Form");
}

function SaveForm() {
	//alert("Save Form");

	var name = $.trim($('#txt_user').val());
	var email = $.trim($('#txt_email').val());
	var pwd = $.trim($('#txt_pwd').val());

	if (name == "" || email == "" || pwd == "")
	{
		alert("Invalid Data ...");
		return false;
	}

	var obj = {
		Username : name,
		Email : email,
		Password : pwd
	};

	//var url = "https://api.github.com/users/xyz/repos";
	var url = "/api/Register/RegisterNewUser";

	$.ajax({
		type: "POST",
		url: url,
		dataType: "json",
		data: JSON.stringify(obj),
		contentType: 'application/json; charset=uft-8',
		success: function (result) {
			console.log(result);
			if (result.statusCode === 200) {
				console.log(result.statusMessage);
				$('#txt_user').val() = "";
				$('#txt_email').val() = "";
				$('#txt_pwd').val() = "";
			}
		},
		error: function (result) {
			console.log(result);
		}
	});






	//fetch("https://jsonplaceholder.typicode.com/posts", {
	//	method: 'post',
	//	body: post,
	//	headers: {
	//		'Accept': 'application/json',
	//		'Content-Type': 'application/json'
	//	}
	//}).then((response) => {
	//	return response.json()
	//}).then((res) => {
	//	if (res.status === 201) {
	//		console.log("Post successfully created!")
	//	}
	//}).catch((error) => {
	//	console.log(error)
	//})




}
