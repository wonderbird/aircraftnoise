namespace AircraftNoise.Core.Domain;

public readonly record struct NoiseMeasurement(DateTime Timestamp, double NoiseMeasurementDba);