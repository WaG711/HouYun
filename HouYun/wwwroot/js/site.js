function toggleSidebar() {
    var sidebar = document.querySelector('.sidebar');
    var container = document.querySelector('.content');

    var columns = document.querySelectorAll('.my-custom-col');
    var cards = document.querySelectorAll('.card');
    var imgs = document.querySelectorAll('.card-img-top');

    var isOpen = sidebar.classList.contains('hidden');

    if (isOpen) {
        sidebar.classList.remove('hidden');
        container.style.marginLeft = '270px';
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

function toggleNotification() {
    $("#notificationPopup").toggle();
}

/*function closeNotificationOutside(event) {
    var notificationPopup = document.getElementById('notificationPopup');
    var notificationButton = document.getElementById('notificationButton');

    if (!notificationPopup.contains(event.target) && event.target !== notificationButton) {
        notificationPopup.style.display = 'none';
        document.removeEventListener('click', closeNotificationOutside);
    }
}

document.addEventListener('click', closeNotificationOutside);*/

$(function () {
    $(document).on('click', '#btnclickChangeUsername', function (e) {
        e.preventDefault();

        var url = $(this).attr('href');
        $.get(url, function (data) {
            $("#modal-bodyChangeUsername").html(data);
            $("#ChangeUsername").modal('show');
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
