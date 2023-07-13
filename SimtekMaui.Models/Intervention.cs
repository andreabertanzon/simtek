namespace SimtekMaui.Models
{
    public class Intervention(string title, DateOnly interventionDate, Site site) : IRecordClass
    {
        public Intervention(Guid id, string title, DateOnly interventionDate, Site site):this(title, interventionDate,site)
        {
            Id = id;
        }

        Guid Id { get; set; }
        public string Title { get; set; } = title;
        public string? Description { get; set; }
        public DateOnly InterventionDate { get; set; } = interventionDate;
        public Site Site { get; set; } = site;
        public bool Synchronized { get; set; } = false;

        public string Identity => Id.ToString();
    }
}
