using MediatR;
using Simtek.Application.Commands;
using Simtek.Application.Repositories.Interfaces;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class CreateCustomerCommandHandler(ICustomerRepository customerRepository):IRequestHandler<CreateCustomerCommand, OperationResult<Unit>>
{
    public async Task<OperationResult<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await customerRepository.CreateCustomerAsync(
                customer: request.Customer,
                cancellationToken: cancellationToken);
            
            return OperationResult<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}