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

function toggleLike(button) {
    var likeButton = document.getElementById("likeButton");
    var removeLikeButton = document.getElementById("removeLikeButton");
    if (button === "like") {
        if (likeButton.innerText === "Like") {
            likeButton.innerText = "Remove Like";
            likeButton.className = "btn btn-danger";
            likeButton.setAttribute("onclick", "toggleButtonState('like')");
            removeLikeButton.style.display = "none";
        } else {
            likeButton.innerText = "Like";
            likeButton.className = "btn btn-primary";
            likeButton.setAttribute("onclick", "toggleButtonState('like')");
            removeLikeButton.style.display = "none";
        }
    } else if (button === "removeLike") {
        if (removeLikeButton.innerText === "Remove Like") {
            removeLikeButton.innerText = "Like";
            removeLikeButton.className = "btn btn-primary";
            removeLikeButton.setAttribute("onclick", "toggleButtonState('removeLike')");
            likeButton.style.display = "none";
        } else {
            removeLikeButton.innerText = "Remove Like";
            removeLikeButton.className = "btn btn-danger";
            removeLikeButton.setAttribute("onclick", "toggleButtonState('removeLike')");
            likeButton.style.display = "none";
        }
    }
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
        .then(response => {
            if (response.ok) {
                setLikeCookie(true);
                toggleButtonState('like');
            }
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
        .then(response => {
            if (response.ok) {
                setLikeCookie(false);
                toggleButtonState('removeLike');
            }
        })
        .catch(error => {
            console.error('Error removing like:', error);
        });
}

function toggleButtonState(button) {
    var likeButton = document.getElementById("likeButton");
    if (button === "like") {
        likeButton.innerText = "Remove Like";
        likeButton.className = "btn btn-danger";
        likeButton.setAttribute("onclick", "removeLike()");
    } else {
        likeButton.innerText = "Like";
        likeButton.className = "btn btn-primary";
        likeButton.setAttribute("onclick", "addLike()");
    }
}

function setLikeCookie(liked) {
    document.cookie = `videoLiked=${liked}; path=/`;
}

function getLikeCookie() {
    const cookieName = "videoLiked=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookieArray = decodedCookie.split(';');
    for (let i = 0; i < cookieArray.length; i++) {
        let cookie = cookieArray[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(cookieName) === 0) {
            return cookie.substring(cookieName.length, cookie.length);
        }
    }
    return null;
}

function setButtonStateFromCookie() {
    const liked = getLikeCookie();
    if (liked === "true") {
        toggleButtonState('like');
    } else if (liked === "false") {
        toggleButtonState('removeLike');
    }
}

setButtonStateFromCookie();

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
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => {
            console.error("Error:", error);
        });
}