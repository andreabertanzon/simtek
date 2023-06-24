using SimtekData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimtekApplication.Handlers.Mappers;

public static class SiteMapperExtensions
{
    public static SimtekDomain.Site ToDomainModel(this SiteDto site, CustomerDto customerDto)
    {
        return new SimtekDomain.Site(
            Id: site.Id,
            Name: site.Name,
            Address: site.Address,
            Customer: customerDto.ToDomainModel());
    }
}