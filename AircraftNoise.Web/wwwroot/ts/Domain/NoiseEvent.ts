export class NoiseEvent {
  private readonly _id: number;
  private readonly _timestampUtc: Date;
  private _noiseLevelDBA: number | null;

  constructor(id: number) {
    this._id = id;
    this._timestampUtc = new Date();
    this._noiseLevelDBA = null;
  }

  public get id(): number {
    return this._id;
  }

  get timestampUtc(): Date {
    return this._timestampUtc;
  }

  get noiseLevelDBA(): number | null {
    return this._noiseLevelDBA;
  }

  set noiseLevelDBA(value: number | null) {
    this._noiseLevelDBA = value;
  }
}
