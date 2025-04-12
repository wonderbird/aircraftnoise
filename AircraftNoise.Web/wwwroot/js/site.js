import {LocationProvider} from "./Adapters/Outbound/LocationProvider.js";
import {NoiseLevelMapper} from "./Adapters/Inbound/NoiseLevelMapper.js";
import {EventView} from "./Views/EventView.js";
import {ApplicationState} from "./Services/ApplicationState.js";

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', () => {
    ApplicationState.initialize();
    EventView.initialize();
    
    LocationProvider.connectUserInterface();
    NoiseLevelMapper.connectUserInterface();
});
