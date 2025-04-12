import {LocationProvider} from "../Adapters/Outbound/LocationProvider.js";

export class MeasurementStationView {
    private readonly presenter: LocationProvider;

    private readonly positionLabel: HTMLSpanElement;
    private readonly positionErrorLabel: HTMLDivElement;
    private readonly positionErrorMessageLabel: HTMLSpanElement;
    private readonly nextMeasurementStationInfo: HTMLDivElement;

    constructor() {
        this.presenter = new LocationProvider(this);

        this.positionLabel = document.querySelector("#position") as HTMLSpanElement;
        this.positionErrorLabel = document.querySelector("#position-error") as HTMLDivElement;
        this.positionErrorMessageLabel = document.querySelector("#position-error-message") as HTMLSpanElement;
        this.nextMeasurementStationInfo = document.querySelector("#next-measurement-station-info") as HTMLDivElement;

        const locateButton = document.querySelector("#locate-button") as HTMLButtonElement;
        
        if (this.positionLabel
            && this.positionErrorLabel
            && this.positionErrorMessageLabel
            && this.nextMeasurementStationInfo
            && locateButton) {
            locateButton.addEventListener("click", this.presenter.getCurrentPosition.bind(this.presenter));
        }
    }

    public static initialize(): void {
        new MeasurementStationView();
    }
    
    public showPosition(position: GeolocationPosition): void {
        const latitude = position.coords.latitude.toFixed(4);
        const longitude = position.coords.longitude.toFixed(4);
        this.positionLabel.textContent = "(" + latitude + ", " + longitude + ") ";

        this.nextMeasurementStationInfo.removeAttribute("hidden");
        this.positionErrorLabel.setAttribute("hidden", "hidden");
    }

    public showError(errorMessage: string): void {
        this.positionErrorMessageLabel.textContent = errorMessage;

        this.positionErrorLabel.removeAttribute("hidden");
        this.nextMeasurementStationInfo.setAttribute("hidden", "hidden");
    }
}
