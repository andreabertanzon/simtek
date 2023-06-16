namespace SimtekDomain;

// public record Intervention(
//     int Id,
//     Site Site,
//     Dictionary<Worker, double> WorkerHours,
//     Dictionary<Material, double> Materials,
//     DateTime InterventionDate,
//     bool Stored = false);
//     
public record MaterialUse(
    Material Material,
    double Quantity
);
public record WorkerHour(
    Worker Worker,
    double Hours
);
public record Intervention(
    int Id,
    Site Site,
    List<WorkerHour> WorkerHours,
    List<MaterialUse> Materials,
    DateTime InterventionDate,
    bool Stored = false);
 