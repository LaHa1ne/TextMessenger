function acceptFriendshipRequest(senderId) {
    $.ajax({
        url: `/Contacts/AcceptFriendshipRequest?senderId=${senderId}`,
        type: "POST",
        success: function (response) {
            if (response.statusCode == 200) {
                let li = $('#friendshipSenderList li[name="' + senderId + '"]');
                $(li).remove();
            }
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