
using AutoMapper;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
         CreateMap<Product, ProductResponse>().ReverseMap();
    }
}
