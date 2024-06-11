using Simtek.Domain;

namespace Simtek.Application.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Domain.Customer>> GetCustomersAsync(Func<Data.Customer, bool>? predicate = null,
        CancellationToken cancellationToken = default);
    Task<Domain.Customer> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateCustomerAsync(Domain.Customer customer, CancellationToken cancellationToken = default);
    Task UpdateCustomerAsync(Domain.Customer customer, CancellationToken cancellationToken = default);
    Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default);
}