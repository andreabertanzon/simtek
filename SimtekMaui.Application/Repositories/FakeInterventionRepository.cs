using SimtekMaui.Data.Models.Intervention;
using SimtekMaui.Data.Repositories.Abstractions;

namespace SimtekMaui.Application.Repositories;

public class FakeInterventionRepository() : IInterventionRepository
{
    private static readonly List<InterventionDto> interventionDtos = new()
        {
            new InterventionDto
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                SiteName = "Condominio Piccia",
                CustomerName = "Loretta Bianchini",
                Title = "Bagni secondo piano",
                Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
                InterventionDate = default,
                Stored = false
            }, new InterventionDto
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                SiteName = "Casa Pidocchi",
                CustomerName = "Luigi Pidocchi",
                Title = "Prese cucina",
                Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
                InterventionDate = default,
                Stored = false
            }, new InterventionDto
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                SiteName = "Condominio Loreto",
                CustomerName = "Piccioni",
                Title = "Bagni secondo piano",
                Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
                InterventionDate = default,
                Stored = false
            }, new InterventionDto
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                SiteName = "Condominio Donatini",
                CustomerName = "Maria Donatini",
                Title = "Bagni secondo piano",
                Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
                InterventionDate = default,
                Stored = false
            }, new InterventionDto
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                SiteName = "Condominio Agilulfo",
                CustomerName = "Rodolfo Rodolfi",
                Title = "Bagni secondo piano",
                Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
                InterventionDate = default,
                Stored = false
            }, new InterventionDto
            {
                Id = Guid.NewGuid(),
                SiteId = Guid.NewGuid(),
                SiteName = "Condominio Piccia",
                CustomerName = "Loretta Bianchini",
                Title = "Bagni secondo piano",
                Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
                InterventionDate = default,
                Stored = false
            },
    };

    public async Task<List<InterventionDto>> GetAsync(CancellationToken cancellationToken)
    {
        return interventionDtos;

    }
}