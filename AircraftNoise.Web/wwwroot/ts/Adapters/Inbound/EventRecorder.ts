import {ApplicationState} from "../../Services/ApplicationState";

export class EventRecorder {
    private static _instance: EventRecorder = new EventRecorder();
    
    private readonly events: HTMLUListElement;
    
    private constructor() {
        this.events = document.querySelector("#events") as HTMLUListElement;
    }
    
    static get instance(): EventRecorder {
        if (!EventRecorder._instance) {
            EventRecorder._instance = new EventRecorder();
        }
        return this._instance;
    }
    
    public static connectUserInterface(): void {
        const recordButton = document.querySelector("#record-button") as HTMLButtonElement;

        if (EventRecorder.instance.events
            && recordButton) {
            recordButton.addEventListener("click", () => {
                ApplicationState.noiseEventRepository.create();

                const event = document.createElement("li");
                let now = new Date();
                event.textContent = now.toLocaleString();
                EventRecorder.instance.events.appendChild(event);
            });
        }
    }
}
