const events = document.querySelector("#events") as HTMLUListElement;
const recordButton = document.querySelector("#record-button") as HTMLButtonElement;

if (events && recordButton) {
    recordButton.addEventListener("click", () => {
        const event = document.createElement("li");
        event.textContent = new Date().toLocaleTimeString();
        events.appendChild(event);
    });
}