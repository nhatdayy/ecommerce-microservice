using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Abstractions;
using Catalog.Core.Repositories;
using Contract.Abstarctions;
using MediatR;

namespace Catalog.Application.Features.Products;

public record GetAllProductsPaginationQuery
(PaginationRequest request) : IQuery<PaginationResult<ProductResponse>>;
internal class GetAllProductsPaginationQueryHandler : IQueryHandler<GetAllProductsPaginationQuery, PaginationResult<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetAllProductsPaginationQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<ProductResponse>>> Handle(GetAllProductsPaginationQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(
       request.request,
       string.IsNullOrEmpty(request.request.SearchTerm)
           ? null
           : p => p.Name.Contains(request.request.SearchTerm));

        var mappedData = _mapper.Map<IList<ProductResponse>>(products.Data);
        var result = new PaginationResult<ProductResponse>(
            products.PageNumber,
            products.PageSize,
            products.Total,
            mappedData
        );

        return Result<PaginationResult<ProductResponse>>.Success(result);
    }

}
