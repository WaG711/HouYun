document.addEventListener('DOMContentLoaded', async function () {
    await channelList();
    await GetUserName();
});

document.addEventListener('DOMContentLoaded', function () {
    var sidebarHidden = localStorage.getItem('hidden');

    if (sidebarHidden === 'true') {
        document.querySelector('.sidebar').classList.add('hidden');
    } else if (sidebarHidden === null) {
        localStorage.setItem('hidden', 'true');
    }

    toggleSidebar();
});

function toggleSidebar() {
    var sidebar = document.querySelector('.sidebar');
    var container = document.querySelector('.content');
    var miniSidebar = document.querySelector('.mini-sidebar');
    var video = document.querySelector('.main-video');

    var isOpen = sidebar.classList.contains('hidden');

    if (isOpen) {
        sidebar.classList.remove('hidden');
        miniSidebar.style.display = 'none';

        if (!video) {
            container.classList.remove('expanded-none');
            container.classList.remove('collapsed');
            container.classList.add('expanded');
        }

        localStorage.setItem('hidden', 'false');
    } else {
        sidebar.classList.add('hidden');

        if (video) {
            container.classList.remove('expanded');
            container.classList.remove('collapsed');
            container.classList.add('expanded-none');
        } else {
            container.classList.remove('expanded');
            container.classList.remove('expanded-none');
            container.classList.add('collapsed');
            miniSidebar.style.display = 'block';
        }

        localStorage.setItem('hidden', 'true');
    }

    var elements = document.querySelectorAll('.card, .card-img-top');
    elements.forEach(function (element) {
        if (isOpen) {
            element.classList.remove('full-width');
        } else {
            element.classList.add('full-width');
        }
    });
}


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

async function GetUserName() {
    try {
        var response = await fetch('/User/GetUserName');
        var userName = await response.text();

        document.getElementById('toggleMenuButton').innerText = userName;
    } catch (error) {
        console.error('Ошибка при получении имени пользователя:', error);
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
        await $.ajax({
            url: url,
            type: 'POST',
            headers: {
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
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

    $(document).on('click', '#btnclickChangeRole', function (e) {
        e.preventDefault();

        var url = $(this).attr('href');
        $.get(url, function (data) {
            $("#modal-bodyChangeRole").html(data);
            $("#ChangeRole").modal('show');
        });
    });

    $("#HidebtnModal").on('click', function () {
        var modalToHide = $(this).attr('data-target');
        $(modalToHide).modal('hide');
    });
});

document.getElementById('searchForm').addEventListener('submit', function (event) {
    var searchTerm = document.getElementById('searchTerm').value.trim();
    localStorage.setItem('searchTerm', searchTerm);
    if (!searchTerm) {
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

function positionPopup() {
    var button = document.getElementById('notificationButton');
    var popup = document.getElementById('notificationPopup');

    var buttonRect = button.getBoundingClientRect();

    var popupWidth = popup.offsetWidth;
    var popupHeight = popup.offsetHeight;

    var leftPosition = buttonRect.left + window.pageXOffset;
    var topPosition = buttonRect.bottom + window.pageYOffset;

    popup.style.left = leftPosition - popupWidth + button.offsetWidth + 'px';
    popup.style.top = topPosition + 'px';
}

window.addEventListener('resize', positionPopup);
window.addEventListener('scroll', positionPopup);

function toggleNotification() {
    var popup = document.getElementById('notificationPopup');
    if (popup.style.display === 'block') {
        popup.style.display = 'none';
    } else {
        popup.style.display = 'block';
        positionPopup();
    }
}