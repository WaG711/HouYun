function toggleMenu(videoId) {
    var menu = document.getElementById('menuItems_' + videoId);
    var computedStyle = window.getComputedStyle(menu);

    if (computedStyle.display === 'none' || computedStyle.display === '') {
        menu.style.display = 'block';
    } else {
        menu.style.display = 'none';
    }
}

function toggleSidebar() {
    var sidebar = document.querySelector('.sidebar');
    var container = document.querySelector('.content');

    var isHidden = sidebar.classList.contains('hidden');

    if (isHidden) {
        sidebar.classList.remove('hidden');
        container.style.marginLeft = '270px';
    }
    else {
        sidebar.classList.add('hidden');
        container.style.marginLeft = '25px';
    }
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
        document.addEventListener("click", closeDropdownOutside);
    } else {
        dropdownMenu.style.display = "none";
        document.removeEventListener("click", closeDropdownOutside);
    }
}

function closeDropdownOutside(event) {
    var dropdownMenu = document.querySelector(".dropdown-menu");
    var button = document.querySelector(".zxc");
    if (!dropdownMenu.contains(event.target) && !button.contains(event.target)) {
        dropdownMenu.style.display = "none";
        document.removeEventListener("click", closeDropdownOutside);
    }
}

function watchLater(videoId) {
    var dropdownMenu = document.querySelector(".dropdown-menu");
    dropdownMenu.style.display = "none";
}


function toggleNotification() {
    var popup = document.getElementById("notificationPopup");
    if (popup.style.display === "none" || popup.style.display === "") {
        popup.style.display = "block";
    } else {
        popup.style.display = "none";
    }
}

function closeNotificationOutside(event) {
    var notificationPopup = document.getElementById('notificationPopup');
    var notificationButton = document.getElementById('notificationButton');

    if (!notificationPopup.contains(event.target) && event.target !== notificationButton) {
        notificationPopup.style.display = 'none';
        document.removeEventListener('click', closeNotificationOutside);
    }
}

document.addEventListener('click', closeNotificationOutside);



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
