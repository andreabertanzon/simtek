using MediatR;
using Simtek.Application.Queries;
using Simtek.Application.Repositories.Interfaces;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class GetCustomersQueryHandler
    (ICustomerRepository customerRepository): IRequestHandler<GetCustomersQuery, OperationResult<IEnumerable<Customer>>>
{
    public async Task<OperationResult<IEnumerable<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customers = await customerRepository.GetCustomersAsync(
                predicate:request.Predicate,
                cancellationToken: cancellationToken);
            
            return OperationResult<IEnumerable<Customer>>.Success(customers);

        }
        catch (Exception e)
        {
            return OperationResult<IEnumerable<Customer>>.Failure(e);
        }
    }
}