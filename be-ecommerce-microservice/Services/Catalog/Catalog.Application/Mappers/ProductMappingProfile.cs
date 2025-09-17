
using AutoMapper;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {


        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Types)) 
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brands));
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, ProductTypeResponse>().ReverseMap();
    }
}
