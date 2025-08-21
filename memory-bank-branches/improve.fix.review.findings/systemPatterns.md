# System Patterns - Aircraft Noise Complaint Assistant

## Architecture Overview

Clean Architecture/Hexagonal Architecture implementation with clear separation between domain logic, infrastructure, and presentation layers.

## Key Technical Decisions

### Backend Architecture
- **ASP.NET Core MVC**: Server-side rendering with REST API endpoints
- **Clean Architecture**: Domain-driven design with ports and adapters
- **Dependency Injection**: Built-in ASP.NET Core container
- **Record Structs**: Immutable domain objects with value semantics

### Frontend Architecture
- **TypeScript ES6 Modules**: Modern JavaScript with strong typing
- **Singleton State Management**: ApplicationState pattern for client-side state
- **Adapter Pattern**: Consistent interface for external integrations
- **View Components**: Separation of concerns between UI and business logic

## Component Relationships

### Core Domain Flow
```
Location → MeasurementStation → NoiseEvents → NoiseLevels → Complaints
```

### Dependency Architecture
```
Web Layer (Controllers/Views)
    ↓
Core Layer (Domain + Ports)
    ↓
Infrastructure Layer (Adapters)
```

### TypeScript Module Structure
```
Domain (NoiseEvent) ← Services (ApplicationState) ← Views (EventView, MeasurementStationView)
                  ↑                                    ↑
            Adapters (Inbound/Outbound)          Browser APIs
```

## Design Patterns in Use

### Backend Patterns
- **Repository Pattern**: Data access abstraction (MeasurementStationRepository)
- **Service Pattern**: Business logic encapsulation (LocationLookupService)
- **Adapter Pattern**: External integration (InMemoryMeasurementProvider)
- **Dependency Injection**: Loose coupling between components

### Frontend Patterns
- **Singleton Pattern**: ApplicationState for global state management
- **Adapter Pattern**: LocationProvider, NoiseEventRepository, NoiseLevelMapper
- **Observer Pattern**: Event-driven UI updates
- **Module Pattern**: ES6 modules for code organization

## Critical Implementation Paths

### Event Recording Flow
1. User interaction → EventRecorder (inbound adapter)
2. EventRecorder → ApplicationState (service layer)
3. ApplicationState → NoiseEventRepository (outbound adapter)
4. Repository → In-memory storage

### Noise Level Mapping Flow
1. EventView → NoiseLevelMapper (outbound adapter)
2. NoiseLevelMapper → PeakNoiseLevelsController (REST API)
3. Controller → InMemoryMeasurementProvider (infrastructure)
4. Provider → DFLD HTML parsing → NoiseMeasurement domain objects

### Location Discovery Flow
1. MeasurementStationView → LocationProvider (browser geolocation)
2. LocationProvider → ApplicationState → LocationLookupService
3. LocationLookupService → MeasurementStationRepository
4. Repository → Hardcoded station data (Rösrath-Forsbach)

## Integration Patterns

### External Data Sources
- **DFLD HTML Parsing**: Scraping approach for measurement data using InMemoryMeasurementProvider
- **Browser Geolocation**: Standard HTML5 API integration
- **Manual Export**: User-driven data transfer to complaint systems
- **Flexible Data Integration**: Supports both test data and live DFLD endpoints

### Error Handling Strategy
- Graceful degradation for missing geolocation
- Fallback to manual station selection
- Client-side validation for event recording

## Testing Patterns

### Backend Testing
- **WebApplicationFactory**: Integration testing for REST endpoints
- **xUnit**: Standard .NET testing framework
- **Dependency Injection**: Mock external dependencies

### Frontend Testing
- **TypeScript Compilation**: Type checking as testing
- **Manual Testing**: Browser-based validation

## Data Flow Patterns

### Immutable Data
- Domain objects as record structs
- Value semantics for reliable state management
- No shared mutable state between components

### Unidirectional Data Flow
- UI events → Services → Repositories → External APIs
- State changes bubble up through event handlers
- Clear data ownership and responsibility boundaries

### DFLD Integration Flow
1. **URL Construction**: Region/Station/Date/Time parameters → DFLD endpoint
2. **HTML Retrieval**: HTTP GET to DFLD measurement page  
3. **Data Extraction**: Parse measurement data from HTML response
4. **Domain Mapping**: External data → NoiseMeasurement record structs