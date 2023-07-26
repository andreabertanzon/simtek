using MediatR;

namespace SimtekMaui.Models.Query;

public record GetSitesQuery():IRequest<Result<List<Site>>>;