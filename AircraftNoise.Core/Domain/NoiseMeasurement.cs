namespace AircraftNoise.Core.Domain;

public readonly record struct NoiseMeasurement(DateTime TimestampUtc, double NoiseMeasurementDba);