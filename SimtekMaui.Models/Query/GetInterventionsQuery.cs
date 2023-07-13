using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimtekMaui.Models.Query
{
    public record GetInterventionsQuery():IRequest<Result<List<Intervention>>>;
}
