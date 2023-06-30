using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.CustomerCQRS;

public record GetCustomersQuery() : IRequest<OneOf<List<Customer>, SimtekError>>
{
    
}