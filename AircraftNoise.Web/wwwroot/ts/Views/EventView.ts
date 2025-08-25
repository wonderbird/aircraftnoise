import { EventRecorder } from "../Adapters/Inbound/EventRecorder.js";
import { NoiseLevelMapper } from "../Adapters/Outbound/NoiseLevelMapper.js";
import { NoiseEvent } from "../Domain/NoiseEvent.js";

export class EventView {
  private readonly eventRecorder: EventRecorder;
  private readonly noiseLevelMapper: NoiseLevelMapper;

  private readonly events: HTMLUListElement;

  constructor() {
    this.eventRecorder = new EventRecorder(this);
    this.noiseLevelMapper = new NoiseLevelMapper(this);

    this.events = document.querySelector("#events") as HTMLUListElement;

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
      // TODO: The noise level should be a value object, which can accept a value of "not available yet" and render itself to a text representation.
      let stringRepresentation = event.timestamp.toLocaleString();

      if (event.noiseLevelDBA !== null) {
        stringRepresentation += `: ${event.noiseLevelDBA} dB(A)`;
      }

      const item = document.createElement("li");
      item.textContent = stringRepresentation;
      this.events.appendChild(item);
    }
  }
}
