const positionLabel = document.querySelector("#position") as HTMLSpanElement;
const locateButton = document.querySelector("#locate-button") as HTMLButtonElement;
const positionErrorLabel = document.querySelector("#position-error") as HTMLSpanElement;
const manualStationSearchInstructions = document.querySelector("#manual-measurement-station-search-instructions") as HTMLDivElement;
const nextMeasurementStationInfo = document.querySelector("#next-measurement-station-info") as HTMLDivElement;

function displayPosition(position: GeolocationPosition) {
    const latitude = position.coords.latitude.toFixed(4);
    const longitude = position.coords.longitude.toFixed(4);
    positionLabel.textContent = "(" + latitude + ", " + longitude + ") ";
    
    manualStationSearchInstructions.setAttribute("hidden", "true");
    nextMeasurementStationInfo.removeAttribute("hidden");
}

function showError(error: GeolocationPositionError) {
    positionErrorLabel.textContent = "Deine Position kann nicht ermittelt werden. Dein Gerät begründet dies so: \"" + error.message + "\"";

    manualStationSearchInstructions.removeAttribute("hidden");
    nextMeasurementStationInfo.setAttribute("hidden", "true");
}

if (positionLabel
    && locateButton
    && manualStationSearchInstructions
    && nextMeasurementStationInfo
    && "geolocation" in navigator) {
    locateButton.addEventListener("click", () => {
        navigator.geolocation.getCurrentPosition(displayPosition, showError)
    });
}