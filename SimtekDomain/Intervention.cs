namespace SimtekDomain;

public record Intervention(
    Guid Id,
    Site Site,
    KeyValuePair<string,double> WorkerHours,
    DateTime InterventionDate,
    bool Stored);