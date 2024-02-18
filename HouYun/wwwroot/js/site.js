function toggleMenu(videoId) {
    var menu = document.getElementById("menuItems_" + videoId);
    if (menu.style.display === "none") {
        menu.style.display = "block";
    } else {
        menu.style.display = "none";
    }
}