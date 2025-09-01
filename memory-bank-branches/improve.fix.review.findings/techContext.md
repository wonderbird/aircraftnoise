# Technical Context - Aircraft Noise Complaint Assistant

## Technology Stack

### Backend Technologies
- **.NET 8.0 LTS**: Latest long-term support version
- **ASP.NET Core MVC**: Web framework with Razor Pages
- **C# 12**: Modern language features with record structs
- **xUnit**: Testing framework for unit and integration tests

### Frontend Technologies
- **TypeScript**: Strongly typed JavaScript with ES6 modules (requires .js extensions in browser imports)
- **Node.js 22.14 LTS**: Development toolchain
- **HTML5**: Modern web standards (geolocation, local storage)
- **CSS3**: Styling with responsive design
- **HtmlAgilityPack**: .NET HTML parsing library for DFLD integration

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
- **DFLD Website Structure**: HTML parsing extracts noise data from `<area>` tag title attributes
- **Browser Geolocation**: Requires HTTPS for geolocation API
- **German Language**: UI and data formats specific to German aviation system
- **DFLD URL Parameters**: R=region, S=station, D=date (dd.MM.yyyy), ZT=start hour
- **DFLD Data Windows**: 2-hour measurement periods constraint data access patterns
- **Flexible Data Source**: InMemoryMeasurementProvider supports both test and live DFLD data

### Performance Considerations
- **HTML Parsing**: Synchronous parsing of DFLD measurement files using regex patterns
- **Client-side Storage**: In-memory storage with no persistence
- **Single Station**: Limited to one measurement station per region
- **TypeScript Compilation**: Selective JS compilation (exclude compiled files, keep site.js)
- **DFLD Parsing Complexity**: Regex pattern extraction from HTML image map areas
- **API Integration**: POST requests with structured request/response models
- **Test Data**: DfldHtml test utility for reliable development and testing

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
- **End-to-End Tests**: Cypress testing with GitHub Actions CI/CD pipeline
- **Automated CI Testing**: Docker-based E2E testing with artifact passing and port conflict resolution
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
- **DFLD HTML Parsing**: Custom parser extracts data from `<area>` tag titles using regex `@"Beschwerde zu (\d{2}:\d{2}:\d{2}) Uhr versenden \[(\d+\.\d+) dBA"`
- **Frontend-Backend Integration**: NoiseLevelMapper calls POST endpoints with structured requests
- **DFLD Integration**: InMemoryMeasurementProvider with comprehensive HTML parsing and test utilities
- **Manual Testing**: Browser-based validation of DFLD endpoints
- **Error Handling**: Graceful degradation for external failures
- **Time Zone Conversion**: Berlin time zone handling for accurate timestamp mapping
- **German Date Parsing**: dd.MM.yyyy format parsing with CultureInfo.InvariantCulture
- **TODO Management**: Development tasks tracked in code comments for test-driven development

## CI/CD Pipeline Patterns

### GitHub Actions Workflow
- **Multi-stage Pipeline**: determine_tag → build → e2e testing with proper dependency management  
- **Docker Artifact Passing**: Build once, test everywhere using GitHub artifact upload/download
- **Environment Variable Handling**: Use `${{ env.VAR }}` syntax for GitHub Actions (not shell `${VAR}`)
- **Port Conflict Resolution**: Avoid conflicting port assignments between Docker containers and test tools
- **Detached Container Mode**: Use `--detach` flag for background container execution during testing

### Cypress Integration Lessons
- **Docker Integration**: `cypress-io/github-action@v6` with containerized application testing
- **Wait Strategy**: Use `wait-on` to ensure application readiness before running tests
- **Port Management**: Avoid `CYPRESS_port` environment variable conflicts with Docker port binding
- **Geolocation Mocking**: Browser geolocation simulation for location-based testing

### Troubleshooting Patterns
- **Environment Variable Expansion**: GitHub Actions syntax differs from shell syntax
- **Port Conflicts**: Docker `--publish` and test framework port usage must not overlap  
- **Container Lifecycle**: Proper container startup/shutdown coordination with test execution
- **Artifact Management**: Efficient Docker image passing between pipeline jobs

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