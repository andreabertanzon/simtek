using System.Text.Json;
using System.Text.Json.Serialization;
using Simtek.Domain;
using Worker = Simtek.Data.Worker;

namespace Simtek.Application.Repositories.Mappings;

public static class InterventionExtensions
{
   public static Domain.Intervention ToDomain(this Data.Intervention intervention, IEnumerable<Worker> workers)
   {
      return new Domain.Intervention
      {
         Id = intervention.Id,
         Description = intervention.Description,
         Date = intervention.Date,
         Operators = workers.Select(iw => new {Key=iw.ToDomain(), Value=intervention.InterventionWorkers.FirstOrDefault(x => x.WorkerId == iw.Id)?.HourSpent ?? 0}).ToDictionary(iw => iw.Key, iw => iw.Value),
         Site = intervention.Site.ToDomain(),
         Materials = JsonSerializer.Deserialize<List<Material>>(intervention.Material ?? "{}") ?? [],
         Notes = intervention.Notes
      };
   } 
   
   public static Data.Intervention ToData(this Domain.Intervention intervention)
   {
      return new Data.Intervention
      {
         Id = intervention.Id,
         Description = intervention.Description,
         Date = intervention.Date,
         Material = JsonSerializer.Serialize(intervention.Materials),
         SiteId = intervention.Site.Id,
         Notes = intervention.Notes
      };
   }
}