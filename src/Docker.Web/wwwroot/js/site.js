// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var toastElList = [].slice.call(document.querySelectorAll('.toast'));
var option = {};
var toastList = toastElList.forEach(function (toastEl) {
    var toast = new bootstrap.Toast(toastEl, option);
    toast.show();
});


