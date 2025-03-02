const userIcon = document.getElementById("userIcon");
const dropdown = document.getElementById("userDropdown");
const loginLink = document.getElementById("loginLink");
const registerLink = document.getElementById("registerLink");
const logoutLink = document.getElementById("logoutLink");

// Simulated authentication check
let isLoggedIn = false; // Change this to `true` if user is logged in

function updateDropdown() {
    if (isLoggedIn) {
        loginLink.style.display = "none";
        registerLink.style.display = "none";
        logoutLink.style.display = "block";
    } else {
        loginLink.style.display = "block";
        registerLink.style.display = "block";
        logoutLink.style.display = "none";
    }
}

// Toggle dropdown menu
userIcon.addEventListener("click", () => {
    dropdown.classList.toggle("active");
});

// Simulate logout action
logoutLink.addEventListener("click", () => {
    isLoggedIn = false;
    updateDropdown();
    alert("Logged out successfully!");
});

// Close dropdown when clicking outside
document.addEventListener("click", (event) => {
    if (!userIcon.contains(event.target) && !dropdown.contains(event.target)) {
        dropdown.classList.remove("active");
    }
});

// Initialize dropdown state
updateDropdown();