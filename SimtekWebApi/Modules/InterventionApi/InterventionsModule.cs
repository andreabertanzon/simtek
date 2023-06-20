using Carter;
using MediatR;
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
                [FromServices] IMediator mediatr,
                CancellationToken cancellationToken) =>
            {
                if (getFull == true)
                {
                    var request = new GetInterventionsQuery();
                    var response = await mediatr.Send<OneOf<List<Intervention>, SimtekError>>(request, cancellationToken);
                    return response.ToHttpResult();
                }

                var shortRequest = new GetShortInterventionsQuery();
                var shortResponse = await mediatr.Send(shortRequest, cancellationToken);
                return shortResponse.ToHttpResult();
            });

        app.MapGet("/{id:int}", async (
            [FromRoute] int id,
            [FromServices] IMediator mediator) =>
        {
            var request = new GetInterventionByIdQuery();
            var result = await mediator.Send(request);
            return result.ToHttpResult();
        });

        app.MapPost("/", async (
            [FromBody] Intervention intervention,
            CancellationToken cancellationToken) =>
        {
            //await interventionRepository.AddInterventionAsync(intervention, cancellationToken: cancellationToken);
            return Results.Ok();
        });
    }
}