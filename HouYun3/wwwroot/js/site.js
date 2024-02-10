document.addEventListener('DOMContentLoaded', function () {
    const player = new Plyr('#videoPlayer', {
        controls: ['play', 'progress', 'current-time', 'mute', 'volume', 'fullscreen']
    });
});