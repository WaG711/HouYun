document.addEventListener('DOMContentLoaded', function () {
    var sidebarHidden = localStorage.getItem('hidden');

    if (sidebarHidden !== 'false') {
        document.querySelector('.sidebar').classList.add('hidden');
        toggleSidebar();
    }
});

async function addView(videoId) {
    try {
        const response = await fetch(`/View/AddView/${videoId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({})
        });

        if (!response.ok) {
            throw new Error(`Ошибка при добавлении просмотра: ${response.status}`);
        }

    } catch (error) {
        console.error('Ошибка при добавлении просмотра:', error);
    }
}

async function AddToWatchHistory(videoId) {
    try {
        const response = await fetch(`/WatchHistory/AddToWatchHistory/${videoId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({})
        });

        if (!response.ok) {
            throw new Error(`Ошибка при добавлении в историю просмотров: ${response.status}`);
        }

    } catch (error) {
        console.error('Ошибка при добавлении в историю просмотров:', error);
    }
}

async function addLike() {
    try {
        var videoId = document.getElementById("videoId").value;
        const response = await fetch("/Like/AddLike?videoId=" + videoId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });

        if (!response.ok) {
            throw new Error(`Ошибка при добавлении лайка: ${response.status}`);
        }

        document.getElementById("likeButton").innerText = "Не нравится";
        document.getElementById("likeButton").setAttribute("onclick", "removeLike()");
    } catch (error) {
        console.error('Ошибка при добавлении лайка:', error);
    }
}

async function removeLike() {
    try {
        var videoId = document.getElementById("videoId").value;
        const response = await fetch("/Like/RemoveLike?videoId=" + videoId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });

        if (!response.ok) {
            throw new Error(`Ошибка при удалении лайка: ${response.status}`);
        }

        document.getElementById("likeButton").innerText = "Нравится";
        document.getElementById("likeButton").setAttribute("onclick", "addLike()");
    } catch (error) {
        console.error('Ошибка при удалении лайка:', error);
    }
}

async function addComment() {
    try {
        var videoId = document.getElementById("videoId").value;
        var commentText = document.getElementById("commentText").value;

        if (!videoId.trim() || !commentText.trim()) {
            return;
        }

        var formData = new FormData();
        formData.append("videoId", videoId);
        formData.append("commentText", commentText);

        const response = await fetch("/Comment/AddComment", {
            method: "POST",
            body: formData,
            headers: {
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });

        if (!response.ok) {
            throw new Error(`Ошибка при отправке комментария: ${response.status}`);
        }

        location.reload();
    } catch (error) {
        console.error("Ошибка:", error);
    }
}

function adjustTextareaHeight() {
    var textarea = document.getElementById('commentText');
    textarea.style.height = 'auto';
    textarea.style.height = textarea.scrollHeight + 'px';
}

document.getElementById('commentText').addEventListener('input', function () {
    adjustTextareaHeight();
});

window.addEventListener('load', function () {
    adjustTextareaHeight();
});