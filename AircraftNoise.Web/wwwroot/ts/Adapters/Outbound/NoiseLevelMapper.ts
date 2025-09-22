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

    let warnings = "";
    let noiseEvents = this.noiseEventRepository.noiseEvents;
    for (const event of noiseEvents) {
      let result = await this.queryNoiseLevel(event.timestampUtc);

      if (typeof result === "string") {
        warnings += result + " ";
      } else {
        event.noiseLevelDba = result;
      }

      this.noiseEventRepository.update(event);
    }

    this.view.update(noiseEvents);
    this.view.showWarnings(warnings);
  }

  private async queryNoiseLevel(timestampUtc: Date): Promise<number | string> {
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
          return "Für einige Störungen konnten keine Messdaten abgerufen werden. Bitte versuche es später noch einmal.";

        default:
          return `Fehler beim Messdatenabruf: HTTP Status ${response.status}: ${response.statusText}`;
      }
    } catch (error) {
      return `Fehler beim Messdatenabruf: HTTP Status ${error}`;
    }
  }
}
