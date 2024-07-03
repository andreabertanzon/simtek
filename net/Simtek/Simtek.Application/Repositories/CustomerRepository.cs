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
            .Include(x=>x.Sites)
            .Where(predicate ?? (_ => true)).ToArray();
        return customers.ToDomain();
    }

    public async Task<Domain.Customer> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var customer = await context
            .Customers
            .Include(x=> x.Sites)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return customer?.ToDomain() ?? throw new Exception();
    }

    public async Task CreateCustomerAsync(Domain.Customer customer, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var existingCustomer = context.Customers
            .FirstOrDefault(x => x.Name == customer.Name && x.Surname == customer.Surname);
        if (existingCustomer is not null)
        {
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.Email = customer.Email ?? "email@mancalemail.com";
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Vat = customer.Vat;
            existingCustomer.Zip = customer.Zip;
            existingCustomer.Name = customer.Name;
            existingCustomer.Surname = customer.Surname;
            
            context.Customers.Update(existingCustomer);
            await context.SaveChangesAsync(cancellationToken);
            return;
        }

        context.Customers.Add(
            new Data.Customer()
            {
                Name = customer.Name?.Trim(),
                Email = customer.Email ?? "email@mancalemail.com",
                Phone = customer.Phone?.Trim(),
                Address = customer.Address?.Trim(),
                City = customer.City?.Trim(),
                Zip = customer.Zip?.Trim(),
                Vat = customer.Vat?.Trim(),
                Surname = customer.Surname.Trim()
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