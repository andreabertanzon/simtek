namespace SimtekDomain;

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
    string Title,
    string Description,
    List<WorkerHour> WorkerHours,
    List<MaterialUse> Materials,
    DateTime InterventionDate,
    bool Stored = false);
 
 public record InterventionShort(
     int Id,
     string SiteName,
     DateTime InterventionDate,
     string Title,
     string Description,
     double HourSpent,
     double TotalWorkerCost,
     double TotalMaterialCost);