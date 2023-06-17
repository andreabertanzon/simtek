using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using SimtekData.Repository;
using SimtekDomain;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;

namespace SimtekWebApi.Modules.InterventionApi;

public class InterventionsModule : CarterModule
{
    public InterventionsModule() : base("/interventions")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",
            async (
                [FromQuery] bool? getFull,
                [FromServices] InterventionRepository interventionRepository,
                [FromServices] IMediator mediatr) =>
            {
                if (getFull == true)
                {
                    var request = new GetInterventionsQuery();
                    var response = await mediatr.Send<OneOf<List<Intervention>, SimtekError>>(request);
                    return response.ToHttpResult<List<Intervention>>();
                }

                var shortRequest = new GetShortInterventionsQuery();
                var shortResponse = await mediatr.Send(shortRequest);
                return shortResponse.ToHttpResult();
            });

        app.MapPost("/", async ([FromServices] InterventionRepository interventionRepository) =>
        {
            var site = new Site(-99, "Andrea Valeggio", "Corte Cittadella 19, Valeggio Sul Mincio, 37067",
                new Customer(-99, "Andrea", "Bertanzon", "Corte Cittadella 19, Valeggio Sul Mincio", null, null, null));
            var intervention = new Intervention(
                -99,
                site,
                "Rifacimento Bagno",
                "Aggiunto rubinetto\nAggiunto bidet",
                new List<WorkerHour>()
                {
                    new(new Worker(1, "Simone", "Bonfante", 30.00), 8)
                },
                new List<MaterialUse>(),
                DateTime.Now,
                false
            );
            await interventionRepository.AddInterventionAsync(intervention, cancellationToken: CancellationToken.None);
            return Results.Ok();
        });
    }
}