function addView(videoId) {
    var csrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

    fetch(`/View/AddView/${videoId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': csrfToken
        },
        body: JSON.stringify({})
    })
}

function AddToWatchHistory(videoId) {
    var csrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

    fetch(`/WatchHistory/AddToWatchHistory/${videoId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': csrfToken
        },
        body: JSON.stringify({})
    })
}

function addLike() {
    var videoId = document.getElementById("videoId").value;
    fetch("/Like/AddLike?videoId=" + videoId, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(() => {
        document.getElementById("likeButton").innerText = "Не нравится";
        document.getElementById("likeButton").setAttribute("onclick", "removeLike()");
    })
    .catch(error => {
        console.error('Error adding like:', error);
    });
}

function removeLike() {
    var videoId = document.getElementById("videoId").value;
    fetch("/Like/RemoveLike?videoId=" + videoId, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(() => {
        document.getElementById("likeButton").innerText = "Нравится";
        document.getElementById("likeButton").setAttribute("onclick", "addLike()");
    })
    .catch(error => {
        console.error('Error removing like:', error);
    });
}

function addComment() {
    var videoId = document.getElementById("videoId").value;
    var commentText = document.getElementById("commentText").value;

    var formData = new FormData();
    formData.append("videoId", videoId);
    formData.append("commentText", commentText);

    fetch("/Comment/AddComment", {
        method: "POST",
        body: formData,
        headers: {
            "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .catch(error => {
        console.error("Error:", error);
    });
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