# Active Context - Aircraft Noise Complaint Assistant

## Current Work Focus

**Primary Objective**: Complete the noise level mapping functionality that connects recorded events to actual DFLD measurement data.

## Recent Changes (Git History Analysis)

### Latest Commit (332029c): "feat(get noise levels): wire frontend to backend"
- **Status**: Major integration milestone achieved
- **Achievement**: Frontend now successfully connected to backend API
- **Key Changes**:
  - NoiseLevelMapper calls POST request to PeakNoiseLevelsController
  - API changed from GET to POST with proper request/response models
  - Test data now returns actual measurements (13.0 dBA) instead of placeholder
  - Sample DFLD HTML data integrated into project
  - HTML parsing partially implemented but incomplete

### Previous Development (50a4e50): "feat(get noise levels, wip): noise levels are read from a DFLD HTML response"
- **Foundation**: Initial HTML parsing framework established
- **Location**: MeasurementFileReader basic structure created

### Infrastructure (8ed264f): "test: add REST controller test project"
- Added comprehensive test coverage for PeakNoiseLevelsController
- Integration testing with WebApplicationFactory
- Foundation for reliable API development

### Architecture (70307c8): "refactor: separate domain and logic from web project"
- Clean architecture implementation complete
- Domain logic moved to AircraftNoise.Core
- Clear separation of concerns established

### Backend Foundation (6002541): "feat(get noise levels, wip): request noise level from backend (constant 42 for now)"
- REST API endpoint structure implemented
- Placeholder logic for development and testing
- Ready for real DFLD integration

## Next Steps

### Immediate Priority
1. **Complete DFLD HTML parsing implementation**
   - Fix incomplete parsing logic in MeasurementFileReader
   - Implement proper area element grouping (time + date pairs)
   - Add robust error handling for HTML parsing failures
   - Connect to live DFLD endpoints instead of static file

2. **Implement event-to-noise-level mapping**
   - Map recorded event timestamps to measurement time periods
   - Handle time zone and precision matching
   - Provide peak noise level identification

3. **Complete end-to-end workflow**
   - Verify complete user journey from event recording to noise level display
   - Test with real DFLD data from Rösrath-Forsbach station
   - Validate HTML parsing stability with live data

### Current Technical State

**Working Components**:
- ✅ Event recording (TypeScript frontend)
- ✅ Geolocation and station discovery
- ✅ REST API structure (PeakNoiseLevelsController with POST endpoints)
- ✅ Domain models and clean architecture
- ✅ Test infrastructure with integration tests
- ✅ Frontend-to-backend integration (NoiseLevelMapper)
- ✅ Request/response models (NoiseMeasurementRequest/Response)

**In Progress**:
- 🔄 DFLD HTML parsing (partially implemented, needs completion)
- 🔄 Area element grouping for time/date extraction
- 🔄 Live DFLD endpoint integration
- 🔄 Event timestamp to measurement mapping

**Blockers/Risks**:
- DFLD HTML structure changes could break parsing (noise data embedded in `<area>` tag titles)
- HTML parsing implementation incomplete (area element grouping not implemented)
- Time zone handling between events and measurements (Berlin time zone conversion required)
- Error handling for network failures and HTML parsing failures
- TypeScript ES6 module complexity (browser requires .js extensions in imports)
- No official DFLD API (HTML scraping only approach)
- TODO comments indicate development gaps requiring test-driven development

## Active Decisions and Considerations

### Technical Decisions
- **HTML Parsing Approach**: Chosen over API integration due to lack of DFLD public API
- **Hardcoded Station Data**: Acceptable for Phase 1 (Cologne/Bonn region focus)
- **In-Memory Storage**: Sufficient for current scope, no persistence needed yet

### User Experience Priorities
- **Real-time Event Recording**: Must work during actual noise disturbances
- **Accurate Noise Mapping**: Critical for complaint legitimacy
- **Simple Workflow**: Minimize friction in complaint preparation

## Important Patterns and Preferences

### Code Quality Standards
- **Clean Architecture**: Maintain clear separation of concerns
- **Immutable Domain Objects**: Continue using record structs
- **Comprehensive Testing**: Maintain test coverage for new features
- **Error Handling**: Graceful degradation for external dependencies

### Development Workflow
- **Mob Programming**: Collaborative development approach
- **Small Commits**: Incremental progress with clear commit messages
- **Feature Branches**: Isolated development for major features

## Learnings and Project Insights

### Technical Insights
- **DFLD Integration Complexity**: HTML parsing extracts noise data from `<area>` tag titles in image maps
- **Time Synchronization**: Critical for accurate event-to-measurement mapping with Berlin time zone conversion
- **Browser Limitations**: Geolocation requires HTTPS and user permission
- **TypeScript ES6 Module Challenges**: Browser ES6 modules require .js extensions in TypeScript imports
- **HTML Parsing Implementation**: Regex pattern `@"Beschwerde zu (\d{2}:\d{2}:\d{2}) Uhr versenden \[(\d+\.\d+) dBA"` extracts timestamps and noise levels
- **DFLD Data Constraints**: 2-hour measurement windows limit data access patterns

### User Experience Insights
- **Immediate Recording**: Users need fast event capture during noise events
- **Data Validation**: Official measurement data significantly strengthens complaints
- **Manual Integration**: Direct API submission to complaint systems not feasible

### Project Evolution
- **Scope Creep Prevention**: Maintaining focus on core complaint assistance workflow
- **Technical Debt**: Clean architecture paying dividends for maintainability
- **External Dependencies**: DFLD HTML structure is a significant risk factor
- **Mob Programming Benefits**: Collaborative development approach improving code quality
- **Configuration Management**: Comprehensive .gitignore and .dockerignore for professional deployment
- **Testing Strategy**: Integration testing with real DFLD data validates HTML parsing accuracy

## Current Sprint Focus

**Definition of Done for Current Feature**:
- [ ] Real DFLD HTML data successfully parsed
- [ ] Event timestamps accurately mapped to noise measurements
- [ ] Peak noise levels identified for event periods
- [ ] End-to-end workflow tested with real data
- [ ] Error handling for parsing failures implemented