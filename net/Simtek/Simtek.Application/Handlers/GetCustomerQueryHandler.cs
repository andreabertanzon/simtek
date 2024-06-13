using MediatR;
using Simtek.Application.Queries;
using Simtek.Application.Repositories.Interfaces;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class GetCustomerQueryHandler(ICustomerRepository customerRepository):IRequestHandler<GetCustomerQuery, OperationResult<Customer>>
{
    public async Task<OperationResult<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerRepository.GetCustomerByIdAsync(
                id:request.Id,
                cancellationToken: cancellationToken);
            
            return OperationResult<Customer>.Success(customer);

        }
        catch (Exception e)
        {
            return OperationResult<Customer>.Failure(e);
        }
    }
}