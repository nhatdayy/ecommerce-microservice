using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Products;

public record GetAllProductsQuery
(): IQuery<IList<ProductResponse>>;

internal class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<Result<IList<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        var productResponses = _mapper.Map<IList<ProductResponse>>(products);
        return Result<IList<ProductResponse>>.Success(productResponses);
    }

}
