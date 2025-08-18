# Aircraft Noise

Motivate responsible parties to reduce aircraft noise

## Development Prerequisites

### Frameworks Required for Developing and Running

Please install the following frameworks to build and run the application:

- [.NET SDK (8.0 LTS)](http://get.dot.net/)
- [Node.js (22.14 lts/jod)](https://nodejs.org/)

### Development Recommendations

#### Test Commit Revert Driven Development

If you have adopted the Test Commit Revert Driven Development (TCRDD) workflow, then consider installing the tools listed at the git gamble website. There you can also find a quick introduction into TCRDD.

[Git Gamble Home](https://git-gamble.is-cool.dev)

#### Automatic Quality Checks and Code Formatting Before Committing

To have automatic code quality checks and code formatting before committing, install [pre-commit](https://pre-commit.com) and install the pre-commit hooks before committing to this repository:

```shell
pre-commit install
```

## Run with Hot Reload

```shell
dotnet watch --project AircraftNoise.Web
```

## Run as Docker Container

```shell
docker build -t aircraftnoise .
docker run -p 8080:8080 aircraftnoise
```

## Running the Tests

```shell
dotnet test
```

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=aircraftnoise) who provide an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.

## Acknowledgements

This project uses code, documentation and ideas generated with the assistance of large language models.
