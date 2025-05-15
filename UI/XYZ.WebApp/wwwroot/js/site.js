// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var BaseUrlAPIProj = function () {
    var WebAPIProject = "https://localhost:7150/api/";
    var BaseUrlIdentityAPI = "https://localhost:5263/";
    const _Array = new Array(WebAPIProject, BaseUrlIdentityAPI);
    return _Array;
}