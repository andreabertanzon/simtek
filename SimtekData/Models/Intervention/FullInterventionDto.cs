namespace SimtekData.Models;

public class FullInterventionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime InterventionDate { get; set; }
    public bool Stored { get; set; }
    
    public int SiteId { get; set; }
    public string SiteName { get; set; }
    public string SiteAddress { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerSurname { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerVat { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhoneNumber { get; set; }

    public int WorkerId { get; set; }
    public string WorkerName { get; set; }
    public string WorkerSurname { get; set; }
    public double WorkerPph { get; set; }
    public double HoursWorked { get; set; }

    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
    public double MaterialPrice { get; set; }
    public string MaterialUnit { get; set; }
    public double MaterialQuantity { get; set; }
}