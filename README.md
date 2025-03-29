# Aircraft Noise

Motivate responsible parties to reduce aircraft noise

## Development Prerequisites    

- [.NET SDK (8.0 LTS)](http://get.dot.net/)
- [Node.js (22.14 lts/jod)](https://nodejs.org/)
- [TypeScript (5.8)](https://www.typescriptlang.org/)

## Run with Hot Reload

The build requires two terminals, one for the dotnet build and one for the typescript build.

The dotnet build is executed in the repository root:

```shell
dotnet watch --project AircraftNoise.Web
```

The typescript build is executed in the `AircraftNoise.Web` directory:

```shell
cd AircraftNoise.Web
tsc --watch
```

## Run as Docker Container

```shell
docker build -t aircraftnoise .
docker run -p 8080:8080 aircraftnoise
```

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=aircraftnoise) who provide
an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.
