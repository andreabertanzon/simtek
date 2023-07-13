using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SimtekMaui.Models.Query
{
    public class GetCustomersQuery:IRequest<Result<List<Customer>>>
    {
    }
}
