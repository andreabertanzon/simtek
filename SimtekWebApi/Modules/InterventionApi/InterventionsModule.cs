using Carter;
using Microsoft.AspNetCore.Mvc;
using SimtekData.Repository;
using SimtekDomain;

namespace SimtekWebApi.Modules.InterventionApi;

public class InterventionsModule : CarterModule
{
    public InterventionsModule() : base("/interventions")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", ([FromServices] InterventionRepository interventionRepository) => interventionRepository.GetInterventions());
        app.MapPost("/", ([FromServices] InterventionRepository interventionRepository) =>
        {

            // var site = new Site(-99, "Andrea Valeggio", "Corte Cittadella 19, Valeggio Sul Mincio, 37067",
            //     new Customer(-99, "Andrea", "Bertanzon", "Corte Cittadella 19, Valeggio Sul Mincio", null, null, null));
            // var intervention = new Intervention(
            //     -99,
            //     site,
            //     new Dictionary<Worker, double>()
            //     {
            //         {new Worker(1, "Simone", "Bonfante", 30.00), 8}
            //     },
            //     new Dictionary<Material, double>(),
            //     DateTime.Now,
            //     false
            // );
            // interventionRepository.AddIntervention(intervention);
        });
    }
}