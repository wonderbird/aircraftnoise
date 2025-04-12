export class NoiseEvent {
    private readonly _id: number;
    private readonly _timestamp: Date;
    private _noiseLevelDBA: number | null;
    
    constructor(id: number) {
        this._id = id;
        this._timestamp = new Date();
        this._noiseLevelDBA = null;
    }
    
    public get id(): number {
        return this._id;
    }

    get timestamp(): Date {
        return this._timestamp;
    }
    
    get noiseLevelDBA(): number | null {
        return this._noiseLevelDBA;
    }
    
    set noiseLevelDBA(value: number | null) {
        this._noiseLevelDBA = value;
    }
}