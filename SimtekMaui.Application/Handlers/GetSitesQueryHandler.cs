using MediatR;
using SimtekMaui.Application.Mappers;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.Models;
using SimtekMaui.Models.Query;

namespace SimtekMaui.Application.Handlers;

public class GetSitesQueryHandler(ISiteRepository siteRepository) : IRequestHandler<GetSitesQuery, Result<List<Site>>>
{
    public async Task<Result<List<Site>>> Handle(GetSitesQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var sites= await siteRepository.GetAllByCustomerAsync(request.CustomerId, cancellationToken);
           return Result<List<Site>>.Success(sites);
        }
        catch (Exception ex)
        {
            return Result<List<Site>>.Failure(ex);
        }
    }
}