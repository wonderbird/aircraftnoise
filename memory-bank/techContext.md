# Technical Context - Aircraft Noise Complaint Assistant

## Technology Stack

### Backend Technologies
- **.NET 8.0 LTS**: Latest long-term support version
- **ASP.NET Core MVC**: Web framework with Razor Pages
- **C# 12**: Modern language features with record structs
- **xUnit**: Testing framework for unit and integration tests

### Frontend Technologies
- **TypeScript**: Strongly typed JavaScript with ES6 modules
- **Node.js 22.14 LTS**: Development toolchain
- **HTML5**: Modern web standards (geolocation, local storage)
- **CSS3**: Styling with responsive design

### Infrastructure
- **Docker**: Containerization for deployment
- **ASP.NET Core Built-in DI**: Dependency injection container
- **File System**: Local data storage for DFLD HTML files

## Development Setup

### Prerequisites
- .NET SDK 8.0 LTS
- Node.js 22.14 LTS (jod)
- Docker (optional, for containerized deployment)

### Key Commands
```bash
# Development with hot reload
dotnet watch --project AircraftNoise.Web

# Run tests
dotnet test

# Docker deployment
docker build -t aircraftnoise .
docker run -p 8080:8080 aircraftnoise
```

### Project Structure
```
AircraftNoise.sln
├── AircraftNoise.Web/       # Web application
├── AircraftNoise.Core/      # Domain logic
├── AircraftNoise.Tests/     # Test suite
└── Data/                    # Sample DFLD data
```

## Technical Constraints

### External Dependencies
- **DFLD Website Structure**: HTML parsing depends on stable page structure
- **Browser Geolocation**: Requires HTTPS for geolocation API
- **German Language**: UI and data formats specific to German aviation system

### Performance Considerations
- **HTML Parsing**: Synchronous parsing of DFLD measurement files
- **Client-side Storage**: In-memory storage with no persistence
- **Single Station**: Limited to one measurement station per region

### Security Requirements
- **HTTPS**: Required for geolocation API access
- **CORS**: Cross-origin requests for DFLD data integration
- **Data Privacy**: No persistent storage of user events

## Dependencies

### NuGet Packages (.NET)
- Microsoft.AspNetCore.App (framework)
- Microsoft.AspNetCore.Mvc.Testing (testing)
- xunit, xunit.runner.visualstudio (testing)

### NPM Dependencies (Node.js)
- TypeScript compiler and toolchain
- ES6 module support

### External APIs
- **DFLD Measurement Stations**: HTML endpoints for noise data
- **Browser Geolocation API**: HTML5 standard
- **No formal API contracts**: Integration via HTML parsing

## Development Patterns

### Code Organization
- **Clean Architecture**: Separation of concerns across layers
- **Dependency Injection**: Constructor injection throughout
- **Immutable Objects**: Record structs for domain models
- **Async/Await**: Asynchronous operations for external calls

### Testing Strategy
- **Integration Tests**: WebApplicationFactory for API testing
- **Unit Tests**: Isolated testing of business logic
- **Manual Testing**: Browser-based validation for UI workflows

### Build and Deployment
- **Docker Multi-stage**: Optimized container builds
- **Hot Reload**: Development productivity with automatic rebuilds
- **Production Ready**: Containerized deployment with proper logging

## Tool Usage Patterns

### Development Tools
- **Visual Studio Code**: Primary IDE with TypeScript support
- **JetBrains Rider**: Alternative IDE with .NET debugging
- **Chrome DevTools**: Frontend debugging and network analysis

### Version Control
- **Git**: Source control with conventional commits
- **Feature Branches**: Development workflow with mob programming

### External Integration
- **DFLD HTML Parsing**: Custom parser for measurement data
- **Manual Testing**: Browser-based validation of DFLD endpoints
- **Error Handling**: Graceful degradation for external failures

## Future Technical Considerations

### Scalability
- Database integration for persistent storage
- Multiple measurement station support
- Caching layer for DFLD data

### Performance
- Asynchronous HTML parsing
- Background data synchronization
- Client-side caching strategies

### Integration
- DFLD API integration (when available)
- Direct complaint submission APIs
- Multi-region support architecture