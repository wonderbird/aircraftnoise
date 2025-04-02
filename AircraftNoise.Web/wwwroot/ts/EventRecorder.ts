const events = document.querySelector("#events") as HTMLUListElement;
const recordButton = document.querySelector("#record-button") as HTMLButtonElement;

if (events && recordButton) {
    recordButton.addEventListener("click", () => {
        const event = document.createElement("li");
        let now = new Date();
        event.textContent = now.toLocaleString();
        events.appendChild(event);
    });
}