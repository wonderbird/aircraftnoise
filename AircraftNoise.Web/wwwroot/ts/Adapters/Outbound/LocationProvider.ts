export class LocationProvider {
    private static _instance: LocationProvider;
    
    private readonly positionLabel: HTMLSpanElement;
    private readonly positionErrorLabel: HTMLSpanElement;
    private readonly manualStationSearchInstructions: HTMLDivElement;
    private readonly nextMeasurementStationInfo: HTMLDivElement;
    
    private constructor() {
        this.positionLabel = document.querySelector("#position") as HTMLSpanElement;
        this.positionErrorLabel = document.querySelector("#position-error") as HTMLSpanElement;
        this.manualStationSearchInstructions = document.querySelector("#manual-measurement-station-search-instructions") as HTMLDivElement;
        this.nextMeasurementStationInfo = document.querySelector("#next-measurement-station-info") as HTMLDivElement;
    }
    
    private static get instance(): LocationProvider {
        if (!this._instance) {
            this._instance = new LocationProvider();
        }
        return this._instance;
    }
    
    public static connectUserInterface(): void {
        const locateButton = document.querySelector("#locate-button") as HTMLButtonElement;
        if (LocationProvider.instance.positionLabel
            && LocationProvider.instance.positionErrorLabel
            && LocationProvider.instance.manualStationSearchInstructions
            && LocationProvider.instance.nextMeasurementStationInfo
            && locateButton
            && "geolocation" in navigator) {
            locateButton.addEventListener("click", () => {
                navigator.geolocation.getCurrentPosition(LocationProvider.instance.displayPosition, LocationProvider.instance.showError)
            });
        }
    }

    private displayPosition(position: GeolocationPosition): void {
        const latitude = position.coords.latitude.toFixed(4);
        const longitude = position.coords.longitude.toFixed(4);
        LocationProvider.instance.positionLabel.textContent = "(" + latitude + ", " + longitude + ") ";

        LocationProvider.instance.manualStationSearchInstructions.setAttribute("hidden", "true");
        LocationProvider.instance.nextMeasurementStationInfo.removeAttribute("hidden");
    }

    private showError(error: GeolocationPositionError): void {
        LocationProvider.instance.positionErrorLabel.textContent = "Deine Position kann nicht ermittelt werden. Dein Gerät begründet dies so: \"" + error.message + "\"";

        LocationProvider.instance.manualStationSearchInstructions.removeAttribute("hidden");
        LocationProvider.instance.nextMeasurementStationInfo.setAttribute("hidden", "true");
    }
}
