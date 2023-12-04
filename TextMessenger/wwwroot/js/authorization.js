function openRegistrationModalForm() {
    let registrationModalForm = $('#RegistrationModalForm');
    console.log(registrationModalForm);
    registrationModalForm.modal('show');
}

function openLoginModalForm() {
    let loginModalForm = $('#LoginModalForm');
    console.log(loginModalForm);
    loginModalForm.modal('show');
}

function Login(loginForm) {
    if ($(loginForm).valid()) {
        $(loginForm).submit();
    }
}

function Registration(registrationForm) {
    if ($(registrationForm).valid()) {
        $(registrationForm).submit();
    }
}