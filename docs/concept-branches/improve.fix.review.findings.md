# Fix Important Design and Code Quality Issues

### TODO Comments in the Code

- AircraftNoise.Core/Domain/NoiseMeasurementRange.cs:18 // TODO: Edge case "no data" is not considered
- AircraftNoise.Web/Controllers/PeakNoiseLevelsController.cs:24 // TODO: Clarify error handling - what happens if there is no data?
- AircraftNoise.Web/wwwroot/ts/Adapters/Outbound/NoiseLevelMapper.ts:31,41,50 // TODO: take end time and duration from the current event

### Side Effects of Code Changes may Break Application Without Notice

An automatic end-to-end test should ensure that the happy path of the desired process works. This would reduce the risk of breaking the process while refactoring significantly.

### Add Logging

The earlier proper logging is introduced, the more long term benefit will be earned!

**Add logging** for debugging and monitoring

### Error Handling Strategy

- **Improve error handling** throughout the frontend with user-friendly messages
- No consistent error handling pattern across layers
- Frontend error handling limited to console logging
- No user-friendly error messages for business failures

We need a robust and user friendly error handling strategy. Design it such that the user stays motivated to finish their desired complaint process.

1. **Fix Empty Collection Handling**
   ```csharp
   public NoiseMeasurement GetPeak()
   {
       if (!_measurements.Any())
           throw new InvalidOperationException("No measurements available");
       return _measurements.OrderByDescending(m => m.NoiseMeasurementDba).First();
   }
   ```

2. **Implement Controller Error Handling**
   ```csharp
   try 
   {
       var range = await _measurementProvider.GetMeasurementsBeforeAsync(endTimeUtc, duration);
       var peak = range.GetPeak();
       return new NoiseMeasurementResponse { /* ... */ };
   }
   catch (InvalidOperationException)
   {
       return new NoiseMeasurementResponse { HasMeasurement = false };
   }
   ```

### Architecture Areas for Improvement

Clarify details of the following findings to understand the underlying problem. Then judge which findings to fix immediately and which findings to move to the improvement backlog.

1. **Domain Model Design**
   - `NoiseMeasurementRange` should be immutable but has mutable list
   - Domain objects lack validation logic
   - Business rules not properly encapsulated

2. **Dependency Management**
   - Static dependencies in ApplicationState singleton
   - Missing abstractions for external HTML parsing
   - Tight coupling to DFLD-specific HTML structure

### Technical Risks

Clarify details of the following findings to understand the underlying problem. Then judge which findings to fix immediately and which findings to move to the improvement backlog.

- **Time zone conversion**: Potential UTC/CET conversion issues
- **Memory usage**: Measurement collections not disposed properly
- **XSS potential**: User-generated event data not properly sanitized

### Maintainability Concerns

1. **TODO Comments Proliferation**
   - Multiple critical TODOs in production code paths
   - Missing validation implementations
   - Incomplete error handling scenarios

2. **Inconsistent Code Style**
   - Mixed nullable DateTime handling patterns
   - Inconsistent async method naming
   - Variable naming inconsistencies, e.g. `DBA` suffixes in TypeScript, `Dba` in CSharp; other inconsistencies need to be identified

### Extensibility Issues

Clarify details of the following findings to understand the underlying problem. Then judge which findings to fix immediately and which findings to move to the improvement backlog.

1. **Tight Coupling to DFLD**
   - HTML parsing logic embedded in business layer
   - URL construction mixed with domain objects
   - No abstraction for different measurement providers

2. **Frontend State Management**
   - Singleton ApplicationState creates global coupling
   - No proper state immutability
   - Event handling tightly coupled to specific UI elements

### Short-term Improvements

Clarify details of the following findings to understand the underlying problem. Then judge which findings to fix immediately and which findings to move to the improvement backlog.

**Implement proper event-to-measurement correlation** in `NoiseLevelMapper`

