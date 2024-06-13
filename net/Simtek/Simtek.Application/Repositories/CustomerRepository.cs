using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Simtek.Application.Repositories.Interfaces;
using Simtek.Application.Repositories.Mappings;
using Simtek.Data;
using Simtek.Domain;

namespace Simtek.Application.Repositories;

public class CustomerRepository(IDbContextFactory<SimtekContext> contextFactory) : ICustomerRepository
{
    public async Task<IEnumerable<Domain.Customer>> GetCustomersAsync(Func<Data.Customer, bool>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var customers = context
            .Customers
            .Where(predicate ?? (_ => true)).ToArray();
        return customers.ToDomain();
    }

    public async Task<Domain.Customer> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var customer = await context
            .Customers
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return customer?.ToDomain() ?? throw new Exception();
    }

    public async Task CreateCustomerAsync(Domain.Customer customer, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        if (context.Customers.Any(x => x.Name == customer.Name && x.Surname == customer.Surname))
        {
            context.Customers.Update(customer.ToData());
            await context.SaveChangesAsync(cancellationToken);
            return;
        }

        context.Customers.Add(
            new Data.Customer()
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Zip = customer.Zip,
                Vat = customer.Vat,
                Surname = customer.Surname
            }
        );
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCustomerAsync(Domain.Customer customer, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.Customers.Update(customer.ToData());
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var customer = await context.Customers.FindAsync(id);
        if (customer is null) return;
        customer.Stored = true;
        await context.SaveChangesAsync(cancellationToken);
    }
}