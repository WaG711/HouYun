function subscribe() {
    document.getElementById("subscribeForm").submit();
}

function unsubscribe() {
    document.getElementById("unsubscribeForm").submit();
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