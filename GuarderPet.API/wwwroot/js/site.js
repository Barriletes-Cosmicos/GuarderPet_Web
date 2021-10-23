// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('#userMenu').css("display", "none");
    $('#userIcon').click(() => {

        if ($('#userMenu').css("display") == "none") {
            $('#userMenu').css("display", "flex");
        }
        else{
            $('#userMenu').css("display", "none");
        }
    });

});