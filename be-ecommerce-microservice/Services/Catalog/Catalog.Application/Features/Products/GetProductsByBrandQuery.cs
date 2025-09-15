using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products;
public record GetProductsByBrandQuery
(string Name): IQuery<IList<ProductResponse>>;

internal class GetProductsByBrandQueryHandler : IQueryHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetProductsByBrandQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<IList<ProductResponse>>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByBrand(request.Name);
        var response = _mapper.Map<IList<ProductResponse>>(products);
        return Result<IList<ProductResponse>>.Success(response);
    }
}
