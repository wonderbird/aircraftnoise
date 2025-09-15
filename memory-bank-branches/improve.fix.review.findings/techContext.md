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
- Development: `dotnet watch --project AircraftNoise.Web`
- Testing: `dotnet test`
- Docker: `docker build -t aircraftnoise .`

## Technical Constraints

### External Dependencies
- **DFLD Website Structure**: HTML parsing extracts noise data from `<area>` tag title attributes
- **Browser Geolocation**: Requires HTTPS for geolocation API
- **German Language**: UI and data formats specific to German aviation system
- **DFLD URL Parameters**: R=region, S=station, D=date (dd.MM.yyyy), ZT=start hour
- **DFLD Data Windows**: 2-hour measurement periods constraint data access patterns
- **Flexible Data Source**: InMemoryMeasurementProvider supports both test and live DFLD data

### Performance Considerations
- HTML parsing using regex patterns
- In-memory storage only
- Single measurement station per region
- Structured request/response models

### Security Requirements
- HTTPS required for geolocation
- No persistent storage of user events
- Input sanitization needed
- Proper error handling without information disclosure

## Dependencies

### NuGet Packages (.NET)
- Microsoft.AspNetCore.App (framework)
- Microsoft.AspNetCore.Mvc.Testing (testing)
- Microsoft.Extensions.Logging (production logging)
- xunit, xunit.runner.visualstudio (testing)

### NPM Dependencies (Node.js)
- TypeScript compiler and toolchain
- ES6 module support

### External APIs
- DFLD measurement stations (HTML parsing)
- Browser Geolocation API

## Development Patterns

### Code Organization
- **Clean Architecture**: Separation of concerns across layers
- **Dependency Injection**: Constructor injection throughout
- **Immutable Objects**: Record structs for domain models
- **Async/Await**: Asynchronous operations for external calls

### Testing Strategy
- Integration tests with WebApplicationFactory
- Unit tests for business logic
- Cypress E2E testing with CI/CD
- Manual browser testing

### Build and Deployment
- Docker multi-stage builds
- Hot reload for development
- Containerized deployment ready

## Tool Usage Patterns

### Development Tools
- Visual Studio Code or JetBrains Rider
- Chrome DevTools for debugging
- Git with conventional commits

### External Integration
- DFLD HTML parsing from area tag titles
- Frontend-backend integration via structured requests
- Time zone conversion (CET to UTC)
- German date format handling


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