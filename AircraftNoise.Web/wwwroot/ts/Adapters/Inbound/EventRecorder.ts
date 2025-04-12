import {EventView} from "../../Views/EventView.js";
import {ApplicationState} from "../../Services/ApplicationState.js";

export class EventRecorder {
    private readonly view: EventView;
    private readonly noiseEventRepository = ApplicationState.noiseEventRepository;
    
    public constructor(view: EventView) {
        this.view = view;
    }
    
    public record(): void {
        this.noiseEventRepository.create();
        this.view.update(this.noiseEventRepository.noiseEvents)
    }
}
