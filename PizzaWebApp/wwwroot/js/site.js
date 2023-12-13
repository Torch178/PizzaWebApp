// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var div = document.getElementById("deliveryAddress");
var del = document.getElementById("deliveryOption");

function showHideDelivery() {
    if (div.style.display == "none") {
        div.style.display = "block";
    }
    else {
        div.style.display = "none";
    }

}
