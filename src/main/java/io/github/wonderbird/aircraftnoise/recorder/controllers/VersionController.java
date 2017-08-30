package io.github.wonderbird.aircraftnoise.recorder.controllers;

import io.github.wonderbird.aircraftnoise.recorder.logic.VersionFileReader;
import io.github.wonderbird.aircraftnoise.recorder.models.VersionInfo;
import io.vertx.core.json.Json;
import io.vertx.ext.web.RoutingContext;

public class VersionController {
    public void getVersion(RoutingContext routingContext) {
        String versionString = new VersionFileReader().read();
        VersionInfo version = new VersionInfo(versionString);

        routingContext.response()
                .putHeader("content-type", "application/json; charset=utf-8")
                .end(Json.encode(version));
    }
}
