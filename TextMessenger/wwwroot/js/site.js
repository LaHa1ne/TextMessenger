// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#LoginModalForm .btn-primary').on('click', function () {
        let loginForm = $('#loginForm');
        Login(loginForm);
    });

    $('#RegistrationModalForm .btn-primary').on('click', function () {
        let registrationForm = $('#registrationForm');
        Registration(registrationForm);
    });
});
