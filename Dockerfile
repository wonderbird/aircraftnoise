FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Node.js LTS
RUN apt-get update && apt-get install -y curl \
    && curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt-get install -y nodejs \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files with preserved directory structure
COPY **/*.csproj ./

# Restore dependencies (this layer caches well)
RUN dotnet restore "AircraftNoise.Web/AircraftNoise.Web.csproj"

# Copy remaining source files (invalidates subsequent layers but preserves restore cache)
COPY . .
WORKDIR "/src/AircraftNoise.Web"
RUN dotnet build "AircraftNoise.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AircraftNoise.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AircraftNoise.Web.dll"]
