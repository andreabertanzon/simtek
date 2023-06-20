using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimtekWebApi.Modules.Material;

public class MaterialModule:CarterModule
{
    public MaterialModule():base("/materials")
    {
        
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", async (
            [FromServices]IMediator mediator,
            [FromRoute] int id) =>
        {
            
            return Results.Ok();
        });
    }
}