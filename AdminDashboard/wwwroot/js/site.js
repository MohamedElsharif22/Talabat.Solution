// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let alertText = $(".alert").text();

if (alertText === null || alertText.trim() === '') {
    $(".alert").hide();
} else {
    $(".alert").show();
}