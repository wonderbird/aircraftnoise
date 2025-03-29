FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM node:22 AS build-typescript
RUN npm install -g typescript
WORKDIR /src
COPY . .
WORKDIR "/src/AircraftNoise.Web"
RUN tsc

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dotnet
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AircraftNoise.Web/AircraftNoise.Web.csproj", "AircraftNoise.Web/"]
RUN dotnet restore "AircraftNoise.Web/AircraftNoise.Web.csproj"
COPY . .
COPY --from=build-typescript /src/AircraftNoise.Web/wwwroot/js /src/AircraftNoise.Web/wwwroot/js
WORKDIR "/src/AircraftNoise.Web"
RUN dotnet build "AircraftNoise.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build-dotnet AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AircraftNoise.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AircraftNoise.Web.dll"]
