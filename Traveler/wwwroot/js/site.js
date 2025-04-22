const exitLink = document.getElementById('exit');
const toggleButton = document.getElementById('profileBtn');

toggleButton.addEventListener('click', function () {
    exitLink.classList.toggle('hidden');
});