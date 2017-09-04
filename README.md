# Aircraft Noise Monitoring - Open Source

[![Build Status](https://travis-ci.org/wonderbird/aircraftnoise.svg?branch=master)](https://travis-ci.org/wonderbird/aircraftnoise)

This web application monitors noise caused by aircrafts.

## License

Copyright (c) Stefan Boos

Licensed under the [MIT](LICENSE.txt) License.

## Application Components

### Aircraft Noise Recorder

The recorder stores new measurements and depicts them in a web application.

## Test Strategy

Tests are executed at three levels:

1. JUnit4 based unit tests check the code on the lowest level.
2. JUnit4 and RestAssured based integration tests check REST APIs.
3. Webdriver.io, Chai and Mocha based end-to-end tests check the web ui and JavaScript.

## Design Decisions

### Why are the UI tests using NodeJS and Webdriver.io instead of Java / Unit Tests and Selenium?

A unit test setting up the FirefoxDriver of selenium has been written. The test launched
a Firefox browser instance successfully but it did not navigate to any url. Instead it
threw a TimeoutException with message "". This issue could not be fixed by downgrading
geckodriver to version 0.17.0 or 0.16.1.

The source code is shown in the following:

#### IndexTest.java
```java
package io.github.wonderbird.aircraftnoise.recorder;

import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxProfile;

import static org.hamcrest.text.MatchesPattern.matchesPattern;
import static org.junit.Assert.assertThat;

public class IndexTest {

    private static WebDriver browser;

    @BeforeClass
    public static void configureSelenium() {
        FirefoxProfile profile = new FirefoxProfile();
        profile.setPreference("xpinstall.signatures.required", false);
        System.setProperty("webdriver.firefox.marionette", "/usr/local/bin/geckodriver");

        browser = new FirefoxDriver();
    }

    @AfterClass
    public static void unconfigureSelenium() {
        if (browser != null) {
            browser.close();
            browser.quit();
        }
    }

    @Test
    public void Should_display_version_number_in_the_footer() {
        browser.get("http://www.heise.de/");
        WebElement versionElement = browser.findElement(By.id("version"));
        String version = versionElement.getText();
        assertThat(version, matchesPattern("^\\d+\\.\\d+\\.\\d+$"));
    }
}
```

#### pom.xml
```xml
<?xml version="1.0" encoding="UTF-8"?>
<project xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xmlns="http://maven.apache.org/POM/4.0.0"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd">

    <!-- ... -->

    <dependencies>
    
        <!-- ... -->
    
        <dependency>
            <groupId>junit</groupId>
            <artifactId>junit</artifactId>
            <version>4.12</version>
            <scope>test</scope>
        </dependency>
        
        <!-- ... -->
        
        <dependency>
            <groupId>org.hamcrest</groupId>
            <artifactId>hamcrest-junit</artifactId>
            <version>2.0.0.0</version>
            <scope>test</scope>
        </dependency>
        <dependency>
            <groupId>org.seleniumhq.selenium</groupId>
            <artifactId>selenium-java</artifactId>
            <version>3.5.3</version>
        </dependency>
    </dependencies>

    <!-- ... -->

</project>
```