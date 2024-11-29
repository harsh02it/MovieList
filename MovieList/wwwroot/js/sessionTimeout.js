(function () {
    let idleTime = 0;
    let sessionTimeout = 1 * 60; // Timeout duration (1 minute in seconds)
    let countdownTimer;

    // Function to update the session timeout timer display in the navbar
    function updateTimerDisplay() {
        let minutes = Math.floor(sessionTimeout / 60);
        let seconds = sessionTimeout % 60;
        let timerElement = document.getElementById("timeout-timer");
        if (timerElement) {
            timerElement.innerText = `Session expires in ${minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
        }
    }

    // Initialize the timer display
    updateTimerDisplay();

    // Update the countdown timer every second
    countdownTimer = setInterval(function () {
        sessionTimeout--;
        updateTimerDisplay();

        // If session times out, log the user out
        if (sessionTimeout <= 0) {
            clearInterval(countdownTimer);
            window.location.href = '/Identity/Account/Logout'; // Redirect to logout
        }
    }, 1000);

    document.body.addEventListener('keydown', function () {
        idleTime = 0;
        sessionTimeout = 1 * 60; // Reset session timeout to 1 minute
    });
})();
