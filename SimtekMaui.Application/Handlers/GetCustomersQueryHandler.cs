using MediatR;
using OneOf;
using SimtekData.Repository.Abstractions;
using SimtekDomain;
using SimtekDomain.CustomerCQRS;
using SimtekDomain.Errors;
using SimtekMaui.Application.Infrastructure;
using SimtekMaui.Application.Mappers;

namespace SimtekMaui.Application.Handlers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery ,OneOf<List<Customer>, SimtekError>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ISimtekService _simtekService;

    public GetCustomersQueryHandler(
        ICustomerRepository customerRepository, 
        HttpClient httpClient, 
        ISimtekService simtekService)
    {
        _customerRepository = customerRepository;
        _simtekService = simtekService;
    }
    public async Task<OneOf<List<Customer>, SimtekError>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // call a rest service to get all the customers
            await _simtekService.CacheCustomersAsync(cancellationToken);

            var localCustomers = await _customerRepository.GetCustomersAsync(cancellationToken);

            var result = localCustomers.Select(x => x.ToDomainModel()).ToList();

            return result;
        }
        catch (Exception ex)
        {
            return new SimtekError(new DatabaseError(ex));
        }
    }
}