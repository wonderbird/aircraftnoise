import {ApplicationState} from "../../Services/ApplicationState";

const noiseLevels = document.querySelector("#noise-levels") as HTMLUListElement;
const getNoiseButton = document.querySelector("#get-noise-button") as HTMLButtonElement;

if (noiseLevels
    && getNoiseButton) {
    getNoiseButton.addEventListener("click", () => {
        const noiseLevel = document.createElement("li");
        const noiseEventRepository = ApplicationState.noiseEventRepository;

        let firstEvent = noiseEventRepository.noiseEvents[0];
        if (!firstEvent) {
            return;
        }

        noiseLevel.textContent = firstEvent.timestamp.toLocaleString();
        noiseLevels.append(noiseLevel);
    });
}