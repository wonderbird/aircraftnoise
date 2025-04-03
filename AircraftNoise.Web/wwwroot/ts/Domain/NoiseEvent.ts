export class NoiseEvent {
    private _timestamp: Date;
    
    constructor() {
        this._timestamp = new Date();
    }

    get timestamp(): Date {
        return this._timestamp;
    }
}