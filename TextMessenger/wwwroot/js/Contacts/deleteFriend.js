function deleteFriend(friendId) {
    $.ajax({
        url: `/Contacts/DeleteFriend?friendId=${friendId}`,
        type: "DELETE",
        success: function (response) {
            if (response.statusCode == 200) {
                let li = $('#friendList li[name="' + friendId + '"]');
                $(li).remove();
            }
        },
        error: function (error) {
            switch (error.statusCode) {
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
