import { EventRecorder } from "../Adapters/Inbound/EventRecorder.js";
import { NoiseLevelMapper } from "../Adapters/Outbound/NoiseLevelMapper.js";
import { NoiseEvent } from "../Domain/NoiseEvent.js";

export class EventView {
  private readonly eventRecorder: EventRecorder;
  private readonly noiseLevelMapper: NoiseLevelMapper;

  private readonly events: HTMLUListElement;
  private readonly missingDataWarning: HTMLElement;

  constructor() {
    this.eventRecorder = new EventRecorder(this);
    this.noiseLevelMapper = new NoiseLevelMapper(this);

    this.events = document.querySelector("#events") as HTMLUListElement;
    this.missingDataWarning = document.getElementById(
      "missing-measurement-data-warning",
    ) as HTMLElement;

    const recordButton = document.querySelector(
      "#record-button",
    ) as HTMLButtonElement;
    const getNoiseButton = document.querySelector(
      "#get-noise-button",
    ) as HTMLButtonElement;

    if (this.events && recordButton && getNoiseButton) {
      recordButton.addEventListener(
        "click",
        this.eventRecorder.record.bind(this.eventRecorder),
      );
      getNoiseButton.addEventListener(
        "click",
        this.noiseLevelMapper.map.bind(this.noiseLevelMapper),
      );
    }
  }

  public static initialize(): void {
    new EventView();
  }

  public update(noiseEvents: NoiseEvent[]): void {
    this.events.innerHTML = "";

    for (const event of noiseEvents) {
      let stringRepresentation = event.timestampUtc.toLocaleString();

      if (event.noiseLevelDba !== null) {
        stringRepresentation += `: ${event.noiseLevelDba} dB(A)`;
      }

      const item = document.createElement("li");
      item.textContent = stringRepresentation;
      this.events.appendChild(item);
    }
  }

  public notifyLoadingEvents(): void {
    if (this.missingDataWarning.hasAttribute("hidden")) {
      return;
    }

    this.missingDataWarning.setAttribute("hidden", "hidden");
  }

  public notifyMissingMeasurementData(): void {
    if (this.missingDataWarning) {
      this.missingDataWarning.removeAttribute("hidden");
    }
  }
}
