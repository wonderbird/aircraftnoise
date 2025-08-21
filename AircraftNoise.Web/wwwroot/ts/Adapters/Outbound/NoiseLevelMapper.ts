import {ApplicationState} from "../../Services/ApplicationState.js";
import {EventView} from "../../Views/EventView.js";

interface NoiseMeasurementResponse {
    noiseMeasurementDba: number;
    timestampUtc: string | null;
    hasMeasurement: boolean;
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
            event.noiseLevelDBA = await this.queryNoiseLevel();
            this.noiseEventRepository.update(event);
        }
        
        this.view.update(noiseEvents);
    }

    private async queryNoiseLevel(): Promise<number | null> {
        try {
            // TODO: take end time and duration from the current event
            const response = await fetch('/PeakNoiseLevels', { 
                method: 'POST',
                headers: { 'Content-Type': 'application/json' }, 
                body: JSON.stringify({ 
                    EndTimeUtc: new Date().toISOString(), 
                    DurationMinutes: 5 
                })
            });

            // TODO: Implement proper error handling if the backend fails to respond
            if (!response.ok) {
                console.error(`Error fetching noise level: ERR ${response.status} - ${response.statusText}`);
                return null;
            }

            const data: NoiseMeasurementResponse = await response.json();
            return data.hasMeasurement ? data.noiseMeasurementDba : null;
        } catch (error) {
            // TODO: Implement proper error handling if there is a communication error
            console.error('Failed to fetch noise level:', error);
            return null;
        }
    }
}
