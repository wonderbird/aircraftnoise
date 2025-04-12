import {ApplicationState} from "../../Services/ApplicationState.js";
import {EventView} from "../../Views/EventView.js";

export class NoiseLevelMapper {
    private view: EventView;
    private readonly noiseEventRepository = ApplicationState.noiseEventRepository;
    
    public constructor(view: EventView) {
        this.view = view;
    }
    
    public map(): void {
        let noiseEvents = this.noiseEventRepository.noiseEvents;
        let firstEvent = noiseEvents[0];

        console.log(`Processed event: ${firstEvent?.timestamp}`);
        
        this.view.update(noiseEvents);
    }
}
