package io.github.wonderbird.aircraftnoise.recorder;

import com.jayway.restassured.RestAssured;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;

import static com.jayway.restassured.RestAssured.get;
import static org.hamcrest.Matchers.equalTo;

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
        get("/api/version").then()
                .assertThat()
                .statusCode(200)
                .body("version", equalTo("Exception: NoSuchFileException"));
    }
}
