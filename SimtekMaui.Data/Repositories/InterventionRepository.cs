using SimtekMaui.Data.Models.Intervention;

namespace SimtekMaui.Data.Repositories;

public class InterventionRepository
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
            }
        };
    }
}