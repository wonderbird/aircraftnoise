package io.github.wonderbird.aircraftnoise.recorder;

import io.github.wonderbird.aircraftnoise.recorder.controllers.DirectoryListController;
import io.github.wonderbird.aircraftnoise.recorder.controllers.MeasurementValueController;
import io.github.wonderbird.aircraftnoise.recorder.controllers.VersionController;
import io.vertx.core.AbstractVerticle;
import io.vertx.core.Future;
import io.vertx.ext.web.Router;
import io.vertx.ext.web.handler.StaticHandler;

/**
 * Aircraft Noise Recorder - Application Startup Class.
 * <p>
 * Sets up the verticle of the Aircraft Noise Recorder.
 */
public class App extends AbstractVerticle {

    @Override
    public void start(Future<Void> fut) {
        Router router = Router.router(vertx);

        DirectoryListController directoryListController = new DirectoryListController();
        router.get("/api/directorylist").handler(directoryListController::listDirectoryContents);

        VersionController versionController = new VersionController();
        router.get("/api/version").handler(versionController::getVersion);

        MeasurementValueController measurementValueController = new MeasurementValueController();
        router.put("/api/measurementvalue").handler(measurementValueController::put);

        router.route().handler(StaticHandler.create());

        int httpPort = config().getInteger("http.port", 8080);
        System.out.println("Configured port: " + httpPort);

        vertx
                .createHttpServer()
                .requestHandler(router::accept)
                .listen(
                        httpPort,
                        result -> {
                            if (result.succeeded()) {
                                fut.complete();
                            } else {
                                fut.fail(result.cause());
                            }
                        }
                );
    }
}