using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimtekDomain.MaterialCQRS;

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
            var request = new GetMaterialByIdQuery(id);
            var response = await mediator.Send(request);
            response.ToHttpResult();
        });
    }
}