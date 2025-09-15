export class NoiseEvent {
  private readonly _id: number;
  private readonly _timestampUtc: Date;
  private _noiseLevelDba: number | null;

  constructor(id: number) {
    this._id = id;
    this._timestampUtc = new Date();
    this._noiseLevelDba = null;
  }

  public get id(): number {
    return this._id;
  }

  get timestampUtc(): Date {
    return this._timestampUtc;
  }

  get noiseLevelDba(): number | null {
    return this._noiseLevelDba;
  }

  set noiseLevelDba(value: number | null) {
    this._noiseLevelDba = value;
  }
}
