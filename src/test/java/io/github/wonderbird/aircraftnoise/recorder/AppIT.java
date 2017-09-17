package io.github.wonderbird.aircraftnoise.recorder;

import io.restassured.RestAssured;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;

public class AppIT {
    @BeforeClass
    public static void configureRestAssured() {
        RestAssured.baseURI = "http://localhost";
        RestAssured.port = Integer.getInteger("http.port", 8080);
    }

    @AfterClass
    public static void unconfigureRestAssured() {
        RestAssured.reset();
    }

    @Test
    public void VersionGet_ShouldReturnAVersionNumber() {
//        get("/api/version").then()
//                .statusCode(200)
//                .body("version", equalTo("0.2.3"));
    }
}
