using System.Text.Json;
using System.Text.Json.Serialization;
using Simtek.Data;

namespace Simtek.Application.Repositories.Mappings;

public static class InterventionExtensions
{
   public static Domain.Intervention ToDomain(this Data.Intervention intervention, IEnumerable<Worker> workers)
   {
      return new Domain.Intervention
      {
         Id = intervention.Id,
         Description = intervention.Description.Split("\n"),
         Date = intervention.Date,
         Operators = workers.Select(iw => new {Key=iw.ToDomain(), Value=iw.PricePerHour}).ToDictionary(iw => iw.Key, iw => iw.Value),
         Site = intervention.Site.ToDomain(),
         Materials = JsonSerializer.Deserialize<Dictionary<string, double>>(intervention.Material ?? "{}") ?? new Dictionary<string, double>()
      };
   } 
}