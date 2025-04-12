import {NoiseEvent} from "../../Domain/NoiseEvent.js";

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
}
