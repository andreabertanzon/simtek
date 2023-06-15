namespace SimtekDomain;

public record Intervention(
    int Id,
    Site Site,
    Dictionary<Worker, double> WorkerHours,
    Dictionary<Material, double> Materials,
    DateTime InterventionDate,
    bool Stored);