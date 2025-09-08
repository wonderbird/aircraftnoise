FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Install Node.js LTS
RUN apt-get update && apt-get install -y curl \
    && curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt-get install -y nodejs \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only solution and project files, preserving directory structure
# See Oleksandr Ktvytskyi's answer in
# https://stackoverflow.com/questions/68597982/net-package-restore-in-docker-cached-separately-from-build/68619620#68619620
COPY *.sln ./
COPY **/*.csproj ./
RUN dotnet sln list \
    | grep ".csproj" \
    | while read -r line; do \
        mkdir -p $(dirname $line); \
        mv $(basename $line) $(dirname $line); \
    done;

RUN dotnet restore "AircraftNoise.Web/AircraftNoise.Web.csproj"

COPY . .
WORKDIR "/src/AircraftNoise.Web"
RUN dotnet build --no-restore "AircraftNoise.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-restore "AircraftNoise.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AircraftNoise.Web.dll"]
