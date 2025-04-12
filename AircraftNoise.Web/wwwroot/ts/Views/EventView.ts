import {EventRecorder} from "../Adapters/Inbound/EventRecorder.js";
import {NoiseEvent} from "../Domain/NoiseEvent.js";

export class EventView {
    private readonly events: HTMLUListElement;
    private readonly presenter: EventRecorder;
    
    constructor() {
        this.presenter = new EventRecorder(this);
        
        this.events = document.querySelector("#events") as HTMLUListElement;
        const recordButton = document.querySelector("#record-button") as HTMLButtonElement;

        if (this.events && recordButton) {
            recordButton.addEventListener("click", this.presenter.record.bind(this.presenter));
        }
    }
    
    public static initialize(): void {
        const view = new EventView();
        view.update([]);
    }
    
    public update(noiseEvents: NoiseEvent[]): void {
        this.events.innerHTML = "";

        for (const event of noiseEvents) {
            const li = document.createElement("li");
            li.textContent = event.timestamp.toLocaleString();
            this.events.appendChild(li);
        }
    }
}
