package io.github.wonderbird.aircraftnoise.recorder.controllers;

import io.vertx.ext.web.RoutingContext;

public class MeasurementValueController {
    public void put(RoutingContext routingContext) {
        routingContext.response()
                .setStatusCode(201)
                .end();
    }
}
