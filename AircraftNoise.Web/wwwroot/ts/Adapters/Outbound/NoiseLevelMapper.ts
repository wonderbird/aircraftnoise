import { ApplicationState } from "../../Services/ApplicationState.js";
import { EventView } from "../../Views/EventView.js";

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
      event.noiseLevelDba = await this.queryNoiseLevel(event.timestampUtc);
      this.noiseEventRepository.update(event);
    }

    this.view.update(noiseEvents);
  }

  // TODO: For the "204: no data" case a warning message should be shown in the UI
  // TODO: For errors, an alert should be shown in the UI
  private async queryNoiseLevel(timestampUtc: Date): Promise<number | null> {
    try {
      const response = await fetch("/PeakNoiseLevel", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          EndTimeUtc: timestampUtc.toISOString(),
          DurationMinutes: 5,
        }),
      });

      switch (response.status) {
        case 200:
          const data: NoiseMeasurementResponse = await response.json();
          return data.noiseMeasurementDba;

        case 204:
          console.log(
            `Received status code ${response.status}: Noise measurements are not available yet.`,
          );
          return null;

        default:
          console.error(
            `Error fetching noise level: ERR ${response.status}: ${response.statusText}`,
          );
          return null;
      }
    } catch (error) {
      console.error("Failed to fetch noise level:", error);
      return null;
    }
  }
}
