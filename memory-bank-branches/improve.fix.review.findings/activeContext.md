# Active Context - Aircraft Noise Complaint Assistant

## Current Work Focus

**Primary Objective**: Production hardening and error handling robustness.

## Current Technical State

**Core Infrastructure Complete ✅**:
- DFLD HTML parsing fully implemented (InMemoryMeasurementProvider)
- Area element grouping working (pairs HTML elements for time/date extraction)
- Time zone conversion (CET to UTC) implemented
- Backend API with proper request/response models
- Frontend-backend integration working
- Peak measurement selection logic implemented
- Comprehensive test infrastructure with DfldHtml test utility
- **Cypress E2E CI Pipeline**: Fully operational with Docker integration and automated testing
- **Event-to-measurement correlation**: Fixed and working correctly with actual event timestamps
- **Clean refactoring**: Controller naming, property naming consistency established
- **Production logging**: Debug logging implemented in controllers

## Next Steps - Current Sprint

### 1. Fix GetPeak() Safety Critical Issue
- **Location**: `NoiseMeasurementRange.cs:18` - `GetPeak()` method crashes on empty measurement data
- **Risk**: Application crashes when no measurement data exists
- **Solution**: Return `Optional<NoiseMeasurement>` or throw meaningful exception with proper handling

### 2. Improve API Design for No-Data Scenarios
- **Location**: `NoiseMeasurementResponse.cs:8` - `HasMeasurement` boolean violates REST principles
- **Issue**: Using boolean flag instead of proper HTTP status codes
- **Solution**: Use HTTP 204 No Content for scenarios with no measurement data

### 3. Complete Frontend Error Handling
- **Location**: `NoiseLevelMapper.ts:40,51` - Incomplete error handling TODOs
- **Issue**: Users see no feedback when backend fails or network issues occur
- **Solution**: Implement user-friendly error messages and graceful degradation

### 4. Add Comprehensive Edge Case Test Coverage
- **Gap**: No unit tests for empty measurement data scenarios
- **Gap**: No tests for network failure cases
- **Gap**: No tests for invalid timestamp ranges
- **Solution**: TDD approach to add failing tests first, then implement proper handling

### 5. Complete End-to-End Workflow Validation
- Validate complete user journey with real DFLD data from Rösrath-Forsbach station
- Ensure all error handling paths work correctly for edge cases

### 6. Complaint Export Feature Implementation
- Implement structured data export for official complaint submission
- Design export format (JSON, CSV, or structured text)
- Connect export to recorded events and their noise level mappings

## Active Decisions and Considerations

### Current Technical Decisions
- **Production Hardening Focus**: Shift from core functionality to error handling robustness
- **TDD for Edge Cases**: Write failing tests first for error scenarios, then implement handling
- **REST API Compliance**: Move away from boolean flags toward proper HTTP status codes
- **User Experience Quality**: Ensure graceful degradation and meaningful error feedback

### Development Standards
- **Clean Architecture**: Continue maintaining clear separation of concerns
- **Immutable Domain Objects**: Keep using record structs for reliable data handling
- **Test-Driven Development**: Address TODO comments with proper test coverage first

## Known Constraints

### External Dependencies
- **Browser Requirements**: Geolocation requires HTTPS and user permission  
- **Manual Export**: Users must manually transfer data to complaint systems (no direct API integration available)

### Technical Limitations
- **In-Memory Storage**: No persistence between sessions (acceptable for current scope)

## Current Sprint Definition of Done

**Completed ✅**:
- [x] End-to-end test written and passing
- [x] Critical event-to-measurement correlation bug fixed
- [x] Clean refactoring completed (controller naming, property consistency)
- [x] Production logging strategy implemented

**In Progress 🔄**:
- [ ] GetPeak() safety issue resolved
- [ ] API design improved for no-data scenarios
- [ ] Frontend error handling completed
- [ ] Comprehensive edge case test coverage added
- [ ] Complete user workflow validated with real DFLD data
- [ ] Complaint export functionality implemented