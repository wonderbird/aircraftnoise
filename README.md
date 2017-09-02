# Aircraft Noise Monitoring - Open Source

[![Build Status](https://travis-ci.org/wonderbird/aircraftnoise.svg?branch=master)](https://travis-ci.org/wonderbird/aircraftnoise)

This web application monitors noise caused by aircrafts.

## Application Components

### Aircraft Noise Recorder

The recorder stores new measurements and depicts them in a web application.

## Test Strategy

Tests are executed at three levels:

1. JUnit4 based unit tests check the code on the lowest level.
2. JUnit4 and RestAssured based integration tests check REST APIs.
3. Webdriver.io, Chai and Mocha based end-to-end tests check the web ui and JavaScript.

## License

Copyright (c) Stefan Boos

Licensed under the [MIT](LICENSE.txt) License.