import {NoiseEvent} from "../../Domain/NoiseEvent";

export class NoiseEventRepository {
    private readonly _noiseEvents: NoiseEvent[] = [];

    constructor() {
        this._noiseEvents = [];
    }

    create(): void {
        this._noiseEvents.push(new NoiseEvent());
    }

    get noiseEvents(): NoiseEvent[] {
        return this._noiseEvents;
    }
}