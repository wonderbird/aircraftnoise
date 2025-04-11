import {LocationProvider} from "./Adapters/Outbound/LocationProvider.js";
import {EventRecorder} from "./Adapters/Inbound/EventRecorder.js";
import {NoiseLevelMapper} from "./Adapters/Inbound/NoiseLevelMapper.js";

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', () => {
    LocationProvider.connectUserInterface();
    EventRecorder.connectUserInterface();
    NoiseLevelMapper.connectUserInterface();
});
