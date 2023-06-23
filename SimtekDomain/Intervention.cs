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
    string SiteName,
    Site? Site,
    string Title,
    string Description,
    List<WorkerHour> WorkerHours,
    List<MaterialUse> Materials,
    DateTime InterventionDate,
    bool Stored = false)
{
    public double TotalMaterialCost => Materials.Sum(m => m.Material.Price * m.Quantity);
    public double TotalWorkerCost => WorkerHours.Sum(w => w.Worker.Pph * w.Hours);
    
    public double TotalCost => TotalMaterialCost + TotalWorkerCost;
    public bool IsFullIntervention => Site is null ;
};