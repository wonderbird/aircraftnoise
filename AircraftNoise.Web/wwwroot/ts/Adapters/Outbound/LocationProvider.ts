import { MeasurementStationView } from "../../Views/MeasurementStationView.js";

export class LocationProvider {
  private readonly view: MeasurementStationView;

  public constructor(view: MeasurementStationView) {
    this.view = view;
  }

  public getCurrentPosition(): void {
    if (!("geolocation" in navigator)) {
      this.view.showError(
        "Dein Browser kann Deine aktuelle Position nicht ermitteln.",
      );
    } else {
      navigator.geolocation.getCurrentPosition(
        this.view.showPosition.bind(this.view),
        this.showError.bind(this),
      );
    }
  }

  private showError(error: GeolocationPositionError): void {
    this.view.showError(error.message);
  }
}
