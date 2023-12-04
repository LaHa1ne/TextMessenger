$(document).ready(function () {
    loadFriendList();

    $('.nav-tabs a').on('shown.bs.tab', function (e) {
        // Получаем идентификатор активной вкладки
        var activeTabId = e.target.id;

        switch (activeTabId) {
            case 'friendList-tab':
                loadFriendList();
                break;
            case 'friendshipSenderList-tab':
                console.log('Вкладка "Заявки" активна');
                loadFriendshipSenderList();
                break;
            case 'sendFriendshipRequestForm-tab':
                console.log('Вкладка "Добавить" активна');
                loadSendFriendshipRequestForm();
                break;
            default:
            // Дополнительная логика по умолчанию
        }
    });
});

function loadFriendList() {
    console.log('Вкладка "Друзья" активна');
    $.ajax({
        url: "/Contacts/GetFriendList",
        type: "GET",
        success: function (data) {
            // Обработка успешного ответа
            $('#friendList').html(data);
            $("#friendList").on("click", ".delete-friend-button", function () {
                var friendId = $(this).closest("li").attr("name");
                deleteFriend(friendId);
            });
        },
        error: function (error) {
            showNotification('error', `Неизвестная ошибка: ${error.status}`);
        }
    });
}

function loadFriendshipSenderList() {
    console.log('Вкладка "Заявки" активна');
    $.ajax({
        url: "/Contacts/GetFriendshipSenderList",
        type: "GET",
        success: function (data) {
            // Обработка успешного ответа
            $('#friendshipSenderList').html(data);
            $("#friendshipSenderList").on("click", ".accept-friendship-request-button", function () {
                var senderId = $(this).closest("li").attr("name");
                acceptFriendshipRequest(senderId);
            });
            $("#friendshipSenderList").on("click", ".reject-friendship-request-button", function () {
                var senderId = $(this).closest("li").attr("name");
                rejectFriendshipRequest(senderId);
            });
        },
        error: function (error) {
            showNotification('error', `Неизвестная ошибка: ${error.status}`);
        }
    });
}

function loadSendFriendshipRequestForm() {
    console.log('Вкладка "Добавить" активна');
    $.ajax({
        url: "/Contacts/GetSendFriendshipRequestForm",
        type: "GET",
        success: function (data) {
            // Обработка успешного ответа
            $('#sendFriendshipRequestForm').html(data);
            $("#sendFriendshipRequestForm").on("click", ".send-friendship-request-button", function () {
                var userNicknameForm = $('#userNicknameForm');
                sendFriendshipRequest(userNicknameForm);
            });
        },
        error: function (error) {
                    showNotification('error', `Неизвестная ошибка: ${error.status}`);
        }
    });
}