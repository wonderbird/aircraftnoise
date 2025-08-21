# Improvement Backlog

This file collects findings and issues about the current code base.

Findings in this file may be important, but not urgent.

After every code review, important but not urgent findings are moved into this file.

## Hardcoded External Dependencies

**Location**: Multiple locations in infrastructure layer  
**Risk**: Scalability and maintainability issues  
**Impact**: Cannot expand beyond Cologne/Bonn region  

- `LocationLookupService`: Hardcoded Cologne coordinates
- `RegionRepository`: Hardcoded "Köln/Bonn" region
- `MeasurementStationRepository`: Hardcoded "Rösrath-Forsbach" station

**Consequence**: System cannot serve other regions without code changes.

### DFLD Integration Fragility

**Location**: `AircraftNoise.Core/Adapters/Outbound/InMemoryMeasurementProvider.cs`  
**Risk**: Complete measurement parsing failure if DFLD changes HTML structure  
**Impact**: Service becomes completely non-functional  

The system depends on:
- Specific `<area>` tag structure in DFLD HTML
- Exact regex pattern matching German text: `(\d+(\.\d+)?) dBA`
- Hardcoded JavaScript function calls in `href` attributes

**Consequence**: Any DFLD website update could break the entire measurement retrieval system.

### Data Integrity Risks

- **HTML parsing failures**: No validation of DFLD HTML structure

**Add Input Validation to HTML Parsing**
```csharp
private static double ParseNoiseLevel(string titleAttribute)
{
    if (string.IsNullOrWhiteSpace(titleAttribute))
        throw new ArgumentException("Title attribute cannot be empty");
    
    var match = Regex.Match(titleAttribute, @"(\d+(\.\d+)?) dBA");
    if (!match.Success)
        throw new FormatException($"Cannot parse noise level from: {titleAttribute}");
        
    return double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
}
```

- **Measurement accuracy**: No validation of parsed noise level values

### Performance Risks

- **Synchronous HTML parsing**: No async processing of large HTML documents
- **N+1 queries**: Frontend loops through events calling backend individually
- **Memory usage**: Measurement collections not disposed properly

### Security Considerations

- **External data trust**: DFLD HTML content processed without validation
- **XSS potential**: User-generated event data not properly sanitized
- **Error information exposure**: Stack traces may be exposed in error responses

### Maintainability Concerns

**Magic Numbers and Strings**

- Hardcoded DFLD URL patterns
- Magic number region and station IDs
- Hardcoded time durations

### Long-term Architectural Improvements

1. **Domain-Driven Design Enhancement**
   - Add proper domain services for business logic
   - Implement domain events for better decoupling
   - Create aggregate roots for complex operations

2. **Resilience Patterns**
   - Circuit breaker for DFLD integration
   - Retry policies for transient failures
   - Fallback mechanisms for service degradation

3. **Testing Strategy**
   - Increase unit test coverage for business logic
   - Add contract testing for DFLD integration
   - Implement end-to-end testing for critical user paths

