using Carter;
using LanguageExt.Common;
using Marten;
using Marten.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using SimtekDomain;

namespace SimtekWebApi.Modules;

public class InterventionsModule : CarterModule
{
    public InterventionsModule() : base("/interventions")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromServices] IDocumentStore store ) =>
        {
            await using var session = store.QuerySession();

            var result = await session.Query<Intervention>().ToListAsync<Intervention>(CancellationToken.None);

            return result.IsEmpty()
                ? Results.NotFound()
                : Results.Ok(result);    
        });

        app.MapPost("/", async ([FromServices] IDocumentStore store) =>
        {
            await using var session = store.LightweightSession();

            var customer = new Customer(
                Id: Guid.NewGuid(),
                Name: "Andrea",
                Surname: "Bertanzon");

            var site = new Site(
                Id: Guid.NewGuid(),
                Name: "Andrea Valeggio",
                Address:new Address("Valeggio sul Mincio", "19", "37067", "Corte Cittadella 19", "VR"), 
                Customer: customer);

            var intervention = new Intervention(
                Id: Guid.NewGuid(),
                Site:site,
                WorkerHours: new("Simone", 30.00),
                InterventionDate: DateTime.Now,
                false);
            
            session.Store(intervention);
            await session.SaveChangesAsync();
            return Results.Ok();
        });
    }
}

public record InterventionRequest(
    Site Site,
    KeyValuePair<Worker, double> WorkerHours,
    DateTime InterventionDate);