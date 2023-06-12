namespace SimtekDomain;

public record Intervention(
    int Id,
    Site Site,
    KeyValuePair<Worker,double> WorkerHours,
    DateTime InterventionDate,
    bool Complete);