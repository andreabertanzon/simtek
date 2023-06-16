using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using SimtekData.Repository;
using SimtekDomain;
using SimtekDomain.Errors;
using SimtekDomain.Query;

namespace SimtekWebApi.Modules.InterventionApi;

public class InterventionsModule : CarterModule
{
    public InterventionsModule() : base("/interventions")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromServices] InterventionRepository interventionRepository, [FromServices] IMediator mediatr) =>
        {
            var request = new GetInterventions();
            var response = await mediatr.Send<OneOf<List<Intervention>,SimtekError>>(request);
            return response.ToHttpResult<List<Intervention>>();
        });
        
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