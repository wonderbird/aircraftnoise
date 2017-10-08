package io.github.wonderbird.aircraftnoise.recorder.controllers;

import io.github.wonderbird.aircraftnoise.recorder.App;
import io.github.wonderbird.aircraftnoise.recorder.models.VersionInfo;
import io.vertx.core.DeploymentOptions;
import io.vertx.core.Vertx;
import io.vertx.core.json.Json;
import io.vertx.core.json.JsonObject;
import io.vertx.ext.unit.Async;
import io.vertx.ext.unit.TestContext;
import io.vertx.ext.unit.junit.VertxUnitRunner;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;

import java.io.IOException;
import java.net.ServerSocket;

@RunWith(VertxUnitRunner.class)
public class VersionControllerTest {
    private Vertx vertx;
    private int port;

    @Before
    public void setup(TestContext context) throws IOException {
        vertx = Vertx.vertx();

        ServerSocket socket = new ServerSocket(0);
        port = socket.getLocalPort();
        socket.close();

        DeploymentOptions options = new DeploymentOptions()
                .setConfig(new JsonObject().put("http.port", port));

        vertx.deployVerticle(App.class.getName(), options, context.asyncAssertSuccess());
    }

    @After
    public void teardown(TestContext context) {
        vertx.close(context.asyncAssertSuccess());
    }

    @Test
    public void get_ShouldReturnVersionNumber(TestContext context) {
        final Async async = context.async();
        vertx.createHttpClient().getNow(port, "localhost", "/api/version", response -> {
                    context.assertEquals(response.statusCode(), 200);
                    response.bodyHandler(body -> {
                        VersionInfo version = Json.decodeValue(body.toString(), VersionInfo.class);
                        String versionNumber = version.getVersion();
                        context.assertTrue(versionNumber.matches("[0-9]*\\.[0-9]*\\.[0-9]*"), String.format("Version number %s does not match the desired format", versionNumber));
                        async.complete();
                    });
                });
    }
}
