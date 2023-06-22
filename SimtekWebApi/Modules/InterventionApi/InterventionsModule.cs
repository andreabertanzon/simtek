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
                [FromServices] IMediator mediatr,
                CancellationToken cancellationToken) =>
            {
                

                var shortRequest = new GetInterventionsQuery();
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