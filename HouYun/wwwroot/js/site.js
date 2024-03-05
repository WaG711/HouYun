document.addEventListener('DOMContentLoaded', async function () {
    await channelList();
});

function toggleSidebar() {
    var sidebar = document.querySelector('.sidebar');
    var container = document.querySelector('.content');

    var columns = document.querySelectorAll('.my-custom-col');
    var cards = document.querySelectorAll('.card');
    var imgs = document.querySelectorAll('.card-img-top');

    var video = document.querySelector('.main-video');

    var isOpen = sidebar.classList.contains('hidden');

    if (isOpen) {
        sidebar.classList.remove('hidden');

        if (!video) {
            container.style.marginLeft = '270px';
        }

        localStorage.setItem('sidebarHidden', 'false');

        columns.forEach(function (column) {
            column.classList.remove('full-width');
        });
        cards.forEach(function (card) {
            card.classList.remove('full-width');
        });
        imgs.forEach(function (img) {
            img.classList.remove('full-width');
        });
    } else {
        sidebar.classList.add('hidden');
        container.style.marginLeft = '25px';
        localStorage.setItem('sidebarHidden', 'true');

        columns.forEach(function (column) {
            column.classList.add('full-width');
        });
        cards.forEach(function (card) {
            card.classList.add('full-width');
        });
        imgs.forEach(function (img) {
            img.classList.add('full-width');
        });
    }
}

document.addEventListener('DOMContentLoaded', function () {
    var sidebarHidden = localStorage.getItem('sidebarHidden');

    if (sidebarHidden === 'true') {
        document.querySelector('.sidebar').classList.add('hidden');
        document.querySelector('.content').style.marginLeft = '25px';
    } else if (sidebarHidden === null) {
        localStorage.setItem('sidebarHidden', 'true');
    }

    toggleSidebar();
});

function toggleMenu() {
    var menuContent = document.getElementById('menuContent');

    if (menuContent.style.display === 'none') {
        menuContent.style.display = 'block';
    } else {
        menuContent.style.display = 'none';
    }
}

document.addEventListener('click', function (event) {
    var menu = document.getElementById('menuContent');
    var button = document.getElementById('toggleMenuButton');
    if (!menu.contains(event.target) && !button.contains(event.target)) {
        menu.style.display = 'none';
    }
});

function toggleDropdown(button) {
    var dropdownMenu = button.nextElementSibling;
    if (dropdownMenu.style.display === "none" || dropdownMenu.style.display === "") {
        dropdownMenu.style.display = "block";
    } else {
        dropdownMenu.style.display = "none";
    }
}

document.addEventListener("click", function (event) {
    if (!event.target.closest('.dropdown')) {
        var dropdownMenus = document.querySelectorAll(".dropdown-menu");
        dropdownMenus.forEach(function (menu) {
            menu.style.display = "none";
        });
    }
});

async function toggleNotification() {
    try {
        const response = await fetch('/Notifications/Index');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.text();
        $("#notificationPopup").toggle();
        $("#notification-list").html(data);
    } catch (error) {
        console.error('Error:', error.message);
    }
}

async function channelList() {
    try {
        const response = await fetch('/Subscription/SubscribedChannels');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.text();
        document.getElementById("channels-list").innerHTML = data;
    } catch (error) {
        console.error('Error:', error.message);
    }
}

async function addToWatchLater(url, videoId) {
    try {
        const csrfToken = $('input[name="__RequestVerificationToken"]').val();

        await $.ajax({
            url: url,
            type: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
            data: { videoId: videoId },
        });
    } catch (error) {
        console.error('Ошибка при добавлении видео в список "Просмотреть позже":', error);
    }
}

$(function () {
    $(document).on('click', '#btnclickChangeUserName', function (e) {
        e.preventDefault();

        var url = $(this).attr('href');
        $.get(url, function (data) {
            $("#modal-bodyChangeUserName").html(data);
            $("#ChangeUserName").modal('show');
        });
    });

    $(document).on('click', '#btnclickChangePassword', function (e) {
        e.preventDefault();

        var url = $(this).attr('href');
        $.get(url, function (data) {
            $("#modal-bodyChangePassword").html(data);
            $("#ChangePassword").modal('show');
        });
    });

    $("#HidebtnModal").on('click', function () {
        var modalToHide = $(this).attr('data-target');
        $(modalToHide).modal('hide');
    });
});

document.getElementById('searchForm').addEventListener('submit', function (event) {
    var searchTerm = document.getElementById('searchTerm').value;
    localStorage.setItem('searchTerm', searchTerm);
    if (!searchTerm.trim()) {
        event.preventDefault();
    }
});

window.onload = function () {
    var searchTerm = localStorage.getItem('searchTerm');
    if (searchTerm) {
        document.getElementById('searchTerm').value = searchTerm;
    }
};

function logout() {
    localStorage.removeItem('searchTerm');
}

async function updateUsername() {
    try {
        var response = await fetch('/api/user/username');
        var username = await response.text();

        document.getElementById('toggleMenuButton').innerText = username;
    } catch (error) {
        console.error('Ошибка при получении имени пользователя:', error);
    }
}

document.addEventListener('DOMContentLoaded', function () {
    updateUsername();
});

document.getElementById('toggleMenuButton').addEventListener('click', function () {
    updateUsername();
});