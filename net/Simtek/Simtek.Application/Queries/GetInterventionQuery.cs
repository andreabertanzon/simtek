using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Queries;

public class GetInterventionQuery:IRequest<OperationResult<Intervention>>
{
    public required int Id { get; set; }
}