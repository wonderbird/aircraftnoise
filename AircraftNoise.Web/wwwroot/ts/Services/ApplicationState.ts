import {NoiseEventRepository} from "../Adapters/Outbound/NoiseEventRepository.js";

export class ApplicationState {
    private static _instance: ApplicationState = new ApplicationState();
    private static readonly _noiseEventRepository: NoiseEventRepository = new NoiseEventRepository();
    
    private constructor() {
    }
    
    static get instance(): ApplicationState {
        if (!ApplicationState._instance) {
            ApplicationState._instance = new ApplicationState();
        }
        return this._instance;
    }
    
    static get noiseEventRepository(): NoiseEventRepository {
        return this._noiseEventRepository;
    }
}
