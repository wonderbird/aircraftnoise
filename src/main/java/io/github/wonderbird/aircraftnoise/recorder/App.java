package io.github.wonderbird.aircraftnoise.recorder;

import io.vertx.core.AbstractVerticle;
import io.vertx.core.Future;
import io.vertx.core.json.Json;
import io.vertx.ext.web.Router;
import io.vertx.ext.web.RoutingContext;
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

        router.get("/api/ls").handler(this::listDirectory);

        router.route().handler(StaticHandler.create());

        vertx
                .createHttpServer()
                .requestHandler(router::accept)
                .listen(
                        config().getInteger("http.port", 8080),
                        result -> {
                            if (result.succeeded()) {
                                fut.complete();
                            } else {
                                fut.fail(result.cause());
                            }
                        }
                );
    }

    private void listDirectory(RoutingContext routingContext) {
        DirectoryList list = new DirectoryListReader().read();
        routingContext.response()
                .putHeader("content-type", "application/json; charset=utf-8")
                .end(Json.encodePrettily(list));
    }
}