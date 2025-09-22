import { ApplicationState } from "../../Services/ApplicationState.js";
import { EventView } from "../../Views/EventView.js";

interface NoiseMeasurementResponse {
  noiseMeasurementDba: number;
  timestampUtc: string | null;
}

export class NoiseLevelMapper {
  private view: EventView;
  private readonly noiseEventRepository = ApplicationState.noiseEventRepository;

  public constructor(view: EventView) {
    this.view = view;
  }

  public async map(): Promise<void> {
    this.view.hideWarnings();

    let warnings: string[] = [];
    let noiseEvents = this.noiseEventRepository.noiseEvents;
    for (const event of noiseEvents) {
      let result = await this.queryNoiseLevel(event.timestampUtc);

      if (typeof result === "string") {
        warnings.push(result);
      } else {
        event.noiseLevelDba = result;
      }

      this.noiseEventRepository.update(event);
    }

    this.view.update(noiseEvents);
    this.view.showWarnings(warnings);
  }

  private async queryNoiseLevel(timestampUtc: Date): Promise<number | string> {
    const forEvent = `Für die Störung am ${timestampUtc.toLocaleDateString()} um ${timestampUtc.toLocaleTimeString()}`;
    let errorSituation = `${forEvent} konnten keine Lärmpegel-Messdaten abgerufen werden`;

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
          return `${forEvent} liegen noch keine Lärmpegel-Messdaten vor.`;

        default:
          return `${errorSituation}: HTTP Status ${response.status}: ${response.statusText}`;
      }
    } catch (error) {
      return `${errorSituation}: HTTP Status ${error}`;
    }
  }
}
