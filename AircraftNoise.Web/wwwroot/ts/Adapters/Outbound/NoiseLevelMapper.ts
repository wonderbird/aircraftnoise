import {ApplicationState} from "../../Services/ApplicationState.js";
import {EventView} from "../../Views/EventView.js";

interface NoiseMeasurementResponse {
    noiseMeasurementDba: number;
}

export class NoiseLevelMapper {
    private view: EventView;
    private readonly noiseEventRepository = ApplicationState.noiseEventRepository;
    
    public constructor(view: EventView) {
        this.view = view;
    }
    
    public async map(): Promise<void> {
        let noiseEvents = this.noiseEventRepository.noiseEvents;

        for (const event of noiseEvents) {
            event.noiseLevelDBA = await this.fetchNoiseLevel();
            this.noiseEventRepository.update(event);
        }
        
        this.view.update(noiseEvents);
    }

    private async fetchNoiseLevel(): Promise<number | null> {
        try {
            const response = await fetch('/PeakNoiseLevels');

            // TODO: Implement proper error handling if the backend fails to respond
            if (!response.ok) {
                console.error('Error fetching noise level:', response.status);
                return null;
            }

            const data: NoiseMeasurementResponse = await response.json();
            return data.noiseMeasurementDba;
        } catch (error) {
            // TODO: Implement proper error handling if there is a communication error
            console.error('Failed to fetch noise level:', error);
            return null;
        }
    }
}
