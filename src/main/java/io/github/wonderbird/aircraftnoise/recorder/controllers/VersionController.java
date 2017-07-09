package io.github.wonderbird.aircraftnoise.recorder.controllers;

import io.github.wonderbird.aircraftnoise.recorder.logic.VersionFileReader;
import io.vertx.core.json.Json;
import io.vertx.ext.web.RoutingContext;

public class VersionController {
    public void getVersion(RoutingContext routingContext) {
        String versionNumber = new VersionFileReader().read();
        routingContext.response()
                .putHeader("content-type", "application/json; charset=utf-8")
                .end(Json.encodePrettily(versionNumber));

    }
}
