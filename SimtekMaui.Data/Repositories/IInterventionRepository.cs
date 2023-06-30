using SimtekMaui.Data.Models.Intervention;
using SimtekMaui.Data.Repositories.Abstractions;

namespace SimtekMaui.Data.Repositories;

public class FakeInterventionRepository : IInterventionRepository
{
    public async Task<List<InterventionDto>> GetAsync()
    {
        return new List<InterventionDto>()
        {
            new InterventionDto
            {
                Id = 1,
                SiteId = 1,
                Title = "Test Intervention",
                Description = "Did something to the house",
                InterventionDate = default,
                Stored = false
            },new InterventionDto
                          {
                              Id = 2,
                              SiteId = 1,
                              Title = "Second Intervention",
                              Description = "Pippo was here",
                              InterventionDate = default,
                              Stored = false
                          }
        };
    }
}