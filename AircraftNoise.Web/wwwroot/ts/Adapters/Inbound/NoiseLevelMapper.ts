import {ApplicationState} from "../../Services/ApplicationState.js";

export class NoiseLevelMapper {
    private static _instance: NoiseLevelMapper = new NoiseLevelMapper();
    private readonly noiseLevels: HTMLUListElement | null;
    private readonly getNoiseButton: HTMLButtonElement | null;
    
    private constructor() {
        this.noiseLevels = document.querySelector("#noise-levels") as HTMLUListElement;
        this.getNoiseButton = document.querySelector("#get-noise-button") as HTMLButtonElement;
    }
    
    static get instance(): NoiseLevelMapper {
        if (!NoiseLevelMapper._instance) {
            NoiseLevelMapper._instance = new NoiseLevelMapper();
        }
        return this._instance;
    }
    
    public static connectUserInterface(): void {
        if (NoiseLevelMapper.instance.noiseLevels
            && NoiseLevelMapper.instance.getNoiseButton) {
            NoiseLevelMapper.instance.getNoiseButton.addEventListener("click", () => {
                const noiseLevel = document.createElement("li");
                const noiseEventRepository = ApplicationState.noiseEventRepository;

                let firstEvent = noiseEventRepository.noiseEvents[0];
                if (!firstEvent) {
                    return;
                }
                
                noiseLevel.textContent = firstEvent.timestamp.toLocaleString();
                NoiseLevelMapper.instance.noiseLevels?.append(noiseLevel);
            });
        }
    }
}
