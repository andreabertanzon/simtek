using MediatR;
using SimtekMaui.Application.Infrastructure;
using SimtekMaui.Application.Mappers;
using SimtekMaui.Models;
using SimtekMaui.Models.Query;
using System.Linq;

namespace SimtekMaui.Application.Handlers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<List<Customer>>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ISimtekService _simtekService;

    public GetCustomersQueryHandler(
        ICustomerRepository customerRepository,
        ISimtekService simtekService)
    {
        _customerRepository = customerRepository;
        _simtekService = simtekService;
    }
    public async Task<Result<List<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // call a rest service to get all the customers
            await _simtekService.CacheCustomersAsync(cancellationToken);

            var localCustomers = await _customerRepository.GetAsync(cancellationToken)!;

            var result = localCustomers.Select(x => x.ToDomainModel()).ToList();

            return Result<List<Customer>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<Customer>>.Failure(ex);
        }
    }
}