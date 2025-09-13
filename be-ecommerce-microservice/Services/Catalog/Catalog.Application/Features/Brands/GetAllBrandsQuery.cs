using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Abstractions;
using Catalog.Core.Repositories;
using Contract.Abstarctions;
using MediatR;

namespace Catalog.Application.Features.Brands;
public record GetAllBrandsQuery
(
    PaginationRequest PaginationRequest
) : IQuery<PaginationResult<BrandResponse>>;

internal class GetAllBrandQueryHandler : IQueryHandler<GetAllBrandsQuery, PaginationResult<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    public GetAllBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }
    public async Task<Result<PaginationResult<BrandResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = await _brandRepository.GetAllAsync(request.PaginationRequest);
        //var response = PaginationResult<BrandResponse>{
        //    Items = _mapper.Map<IEnumerable<BrandResponse>>(result),
        //    PageIndex = result.PageIndex,
        //    PageSize = result.PageSize,
        //    TotalCount = result.TotalCount,
        //    TotalPages = result.TotalPages
        //};
        //return Result<PaginationResult<BrandResponse>>.Success(result);
    }
}
