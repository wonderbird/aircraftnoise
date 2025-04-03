import { applicationState } from "../../Services/ApplicationState";

const noiseLevels = document.querySelector("#noise-levels") as HTMLUListElement;
const getNoiseButton = document.querySelector("#get-noise-button") as HTMLButtonElement;

if (noiseLevels
    && getNoiseButton) {
    getNoiseButton.addEventListener("click", () => {
        const noiseLevel = document.createElement("li");
        noiseLevel.textContent = applicationState.toString();
        noiseLevels.append(noiseLevel);
    });
}