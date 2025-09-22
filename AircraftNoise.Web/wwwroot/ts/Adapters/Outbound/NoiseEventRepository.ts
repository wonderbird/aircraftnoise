import { NoiseEvent } from "../../Domain/NoiseEvent.js";

export class NoiseEventRepository {
  private readonly _noiseEventMap: Map<number, NoiseEvent>;

  constructor() {
    this._noiseEventMap = new Map<number, NoiseEvent>();
  }

  create(): void {
    const id = this._noiseEventMap.size;
    const event = new NoiseEvent(id);

    this._noiseEventMap.set(id, event);
  }

  get noiseEvents(): NoiseEvent[] {
    return Array.from(this._noiseEventMap.values());
  }

  update(event: NoiseEvent) {
    this._noiseEventMap.set(event.id, event);
  }

  moveEventsToMeasurementDataRange(): void {
    const measurementDataStartCet = new Date(2025, 3, 9, 0, 0, 0, 0);

    for (const event of this._noiseEventMap.values()) {
      const correctedHours = event.timestampCet.getHours() % 2;

      const correctedTimestampCet = new Date(
        measurementDataStartCet.getFullYear(),
        measurementDataStartCet.getMonth(),
        measurementDataStartCet.getDate(),
        correctedHours,
        event.timestampCet.getMinutes(),
        event.timestampCet.getSeconds(),
        event.timestampCet.getMilliseconds(),
      );

      this._noiseEventMap.set(
        event.id,
        new NoiseEvent(event.id, correctedTimestampCet),
      );
    }
  }
}
