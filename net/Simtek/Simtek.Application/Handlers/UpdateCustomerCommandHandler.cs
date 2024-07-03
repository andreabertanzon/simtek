using MediatR;
using Simtek.Application.Commands;
using Simtek.Application.Repositories.Interfaces;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository): IRequestHandler<UpdateCustomerCommand, OperationResult<Unit>>
{
    public async Task<OperationResult<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await customerRepository.UpdateCustomerAsync(request.Customer, cancellationToken);
            return OperationResult<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return OperationResult<Unit>.Failure(e);
        }
    }
}