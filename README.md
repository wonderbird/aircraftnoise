# Aircraft Noise

Motivate responsible parties to reduce aircraft noise

## Development Prerequisites

### Frameworks Required for Developing and Running

Please install the following frameworks to build and run the application:

- [.NET SDK (8.0 LTS)](http://get.dot.net/)
- [Node.js (22.14 lts/jod)](https://nodejs.org/)

After having installed the frameworks, run

```shell
dotnet tool restore
```

to install the dotnet tools listed in [.config/dotnet-tools.json](.config/dotnet-tools.json).

### Development Recommendations

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

To explore test reliability and code coverage you can run [Stryker Mutator](https://stryker-mutator.io/docs/stryker-net/getting-started/):

```shell
cd AircraftNoise.Core.Tests
dotnet stryker
```

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=aircraftnoise) who provide an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.

## Acknowledgements

This project uses code, documentation and ideas generated with the assistance of large language models.
