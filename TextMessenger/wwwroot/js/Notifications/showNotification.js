function showNotification(type, text) {
    Swal.fire({
        position: 'top-end',
        icon: type,
        title: text,
        showConfirmButton: false,
        timer: 10000
    });
}