﻿$(document).ready(function () {
    $('#updateForm').submit(function (event) {
        event.preventDefault();

        var formData = new FormData($(this)[0]);

        $.ajax({
            url: '@Url.Action("Update", "Channel")',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    $('#update').modal('hide');
                    $('#updateForm')[0].reset();
                    location.reload();
                } else {
                    $('#modal-bodyUpdate').html(response);
                }
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });

    $('#update').on('hidden.bs.modal', function (e) {
        $('#updateForm')[0].reset();
    });
});