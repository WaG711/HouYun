function toggleMenu(videoId) {
    var menu = document.getElementById("menuItems_" + videoId);
    if (menu.style.display === "none") {
        menu.style.display = "block";
    } else {
        menu.style.display = "none";
    }
}

function toggleSidebar() {
    var sidebar = document.querySelector('.sidebar');
    var container = document.querySelector('.container');

    var computedStyle = window.getComputedStyle(sidebar);
    
    if (computedStyle.display === 'none' || computedStyle.display === '') {
        sidebar.style.display = 'block';
        container.style.marginLeft = '292px';
    }
    else {
        sidebar.style.display = 'none';
        container.style.marginLeft = '25px';
    }
}

function toggleMenu() {
    var menuContent = document.getElementById('menuContent');
    if (menuContent.style.display === 'none' || menuContent.style.display === '') {
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






