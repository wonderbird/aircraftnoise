package io.github.wonderbird.aircraftnoise.recorder.controllers;

import io.github.wonderbird.aircraftnoise.recorder.logic.FileSystemDirectoryListReader;
import io.github.wonderbird.aircraftnoise.recorder.models.DirectoryList;
import io.vertx.core.json.Json;
import io.vertx.ext.web.RoutingContext;

public class DirectoryListController {
    public void listDirectoryContents(RoutingContext routingContext) {
        DirectoryList list = new FileSystemDirectoryListReader().read();
        routingContext.response()
                .putHeader("content-type", "application/json; charset=utf-8")
                .end(Json.encodePrettily(list));
    }
}
