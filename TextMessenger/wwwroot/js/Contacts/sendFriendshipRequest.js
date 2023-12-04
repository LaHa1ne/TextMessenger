function sendFriendshipRequest(userNicknameForm) {
    if ($(userNicknameForm).valid()) {
        $.ajax({
            type: $(userNicknameForm).attr('method'),
            url: $(userNicknameForm).attr('action'),
            data: $(userNicknameForm).serialize(),
            success: function (response) {
                switch (response.statusCode) {
                    case 200:
                        $('#userNicknameForm input').val('');
                        showNotification('info', response.description);
                        break;
                    case 201:
                        $('#userNicknameForm input').val('');
                        showNotification('success', response.description);
                        break;
                    default:
                        $('#sendFriendshipRequestForm').html(response);
                        break;
                }
                console.log(response);
            },
            error: function (error) {
                switch (error.status) {
                    case 404:
                        showNotification('error', 'Пользователь не найден');
                        break;
                    case 500:
                        showNotification('error', 'Внутренняя ошибка сервера');
                        break;
                    default:
                        showNotification('error', 'Неизвестная ошибка');
                        break;
                }
            }
        });
    }
}