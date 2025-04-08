import {ApplicationState} from "../../Services/ApplicationState.js";

export class NoiseLevelMapper {
    private static _instance: NoiseLevelMapper = new NoiseLevelMapper();
    private noiseLevels: HTMLUListElement | null;
    private getNoiseButton: HTMLButtonElement | null;
    
    private constructor() {
        this.noiseLevels = document.querySelector("#noise-levels") as HTMLUListElement;
        this.getNoiseButton = document.querySelector("#get-noise-button") as HTMLButtonElement;
        this.connectUserInterface();
    }
    
    static get instance(): NoiseLevelMapper {
        if (!NoiseLevelMapper._instance) {
            NoiseLevelMapper._instance = new NoiseLevelMapper();
        }
        return this._instance;
    }
    
    private connectUserInterface(): void {
        if (this.noiseLevels && this.getNoiseButton) {
            this.getNoiseButton.addEventListener("click", () => {
                const noiseLevel = document.createElement("li");
                const noiseEventRepository = ApplicationState.noiseEventRepository;

                let firstEvent = noiseEventRepository.noiseEvents[0];
                if (!firstEvent) {
                    return;
                }

                noiseLevel.textContent = firstEvent.timestamp.toLocaleString();
                this.noiseLevels?.append(noiseLevel);
            });
        }
    }
}
