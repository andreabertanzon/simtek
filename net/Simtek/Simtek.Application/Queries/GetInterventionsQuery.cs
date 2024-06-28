using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Queries;

public class GetInterventionsQuery:IRequest<OperationResult<IEnumerable<Intervention>>>
{
    public int? CustomerId { get; set; }
    
    public required int Page { get; set; }
    
    public required int PageSize { get; set; }
    public bool IncludeCustomer { get; set; }
    
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}