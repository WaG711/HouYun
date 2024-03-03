async function subscribe() {
    var channelId = document.getElementById("channelId").value;
    try {
        await fetch("/Subscription/Subscribe?channelId=" + channelId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });
        document.getElementById("subscribeButton").innerText = "Отписаться";
        document.getElementById("subscribeButton").setAttribute("onclick", "unsubscribe()");
        channelList();
    } catch (error) {
        console.error('Error subscribe:', error);
    }
}

async function unsubscribe() {
    var channelId = document.getElementById("channelId").value;
    try {
        await fetch("/Subscription/Unsubscribe?channelId=" + channelId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });
        document.getElementById("subscribeButton").innerText = "Подписаться";
        document.getElementById("subscribeButton").setAttribute("onclick", "subscribe()");
        channelList();
    } catch (error) {
        console.error('Error unsubscribe:', error);
    }
}

$(function () {
    $("#btnclickUpdate").on('click', function () {
        var url = $(this).data('url');
        $.get(url, function (data) {
            $("#modal-bodyUpdate").html(data);
            $("#update").modal('show');
        });
    });

    $("#btnclickDelete").on('click', function () {
        var url = $(this).data('url');
        $.get(url, function (data) {
            $("#modal-bodyDelete").html(data);
            $("#delete").modal('show');
        });
    });

    $("#btnclickAdd").on('click', function () {
        var url = $(this).data('url');
        $.get(url, function (data) {
            $("#modal-bodyAdd").html(data);
            $("#upload").modal('show');
        });
    });

    $("#HidebtnModal").on('click', function () {
        var modalToHide = $(this).attr('data-target');
        $(modalToHide).modal('hide');
    });
});