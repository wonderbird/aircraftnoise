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

        for (const event of noiseEvents) {
            // TODO: use the nearest measurement station to map the timestamp to a noise level
            event.noiseLevelDBA = 0;
            this.noiseEventRepository.update(event);
        }
        
        this.view.update(noiseEvents);
    }
}
