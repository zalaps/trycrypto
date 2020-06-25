// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function OnVerifyClick() {
    var data = $("#verifyform").serialize();
    $.ajax({
        url: "Verify",
        method: "POST",
        data: data,
        success: function (result) {
            $("#verifymsg").html(result);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response);
        }  
    });
    //window.location = "Customer/Details/" + id;
}