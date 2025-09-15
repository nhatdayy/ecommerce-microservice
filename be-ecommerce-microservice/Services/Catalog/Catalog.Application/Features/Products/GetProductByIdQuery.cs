using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products;

public record GetProductByIdQuery
(string Id): IQuery<ProductResponse?>;

internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<Result<ProductResponse?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        var response = _mapper.Map<ProductResponse?>(product);
        return Result<ProductResponse?>.Success(response);
    }
}
