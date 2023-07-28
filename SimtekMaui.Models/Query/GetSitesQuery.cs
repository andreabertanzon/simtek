using MediatR;

namespace SimtekMaui.Models.Query;

public record GetSitesQuery(Guid CustomerId):IRequest<Result<List<Site>>>;