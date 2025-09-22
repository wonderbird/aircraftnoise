export class NoiseEvent {
  private readonly _id: number;
  private readonly _timestampCet: Date;
  private _noiseLevelDba: number | null;

  constructor(id: number) {
    this._id = id;
    this._timestampCet = new Date();
    this._noiseLevelDba = null;
  }

  public get id(): number {
    return this._id;
  }

  get timestampCet(): Date {
    return this._timestampCet;
  }

  get noiseLevelDba(): number | null {
    return this._noiseLevelDba;
  }

  set noiseLevelDba(value: number | null) {
    this._noiseLevelDba = value;
  }
}
