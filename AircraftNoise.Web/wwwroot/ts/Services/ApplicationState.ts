import {NoiseEventRepository} from "../Adapters/Outbound/NoiseEventRepository.js";

export class ApplicationState {
    private static _instance: ApplicationState;
    private static readonly _noiseEventRepository: NoiseEventRepository = new NoiseEventRepository();
    
    private constructor() {
    }
    
    public static initialize(): void {
        ApplicationState._instance = new ApplicationState();
    }
    
    static get instance(): ApplicationState {
        if (!ApplicationState._instance) {
            throw new Error("You must call ApplicationState.initialize() before creating any objects using ApplicationState. It is best to call ApplicationState.initialize() at the beginning of the main entry point of your application.");
        }
        return this._instance;
    }
    
    static get noiseEventRepository(): NoiseEventRepository {
        return this._noiseEventRepository;
    }
}
