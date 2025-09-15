using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Abstractions;
using Catalog.Core.Repositories;
using Contract.Abstarctions;
using MediatR;

namespace Catalog.Application.Features.Brands;
public record GetAllBrandsQuery() : IQuery<IList<BrandResponse>>;

internal class GetAllBrandQueryHandler : IQueryHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    public GetAllBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }

    public async Task<Result<IList<BrandResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = await _brandRepository.GetAllAsync();
        var mappedResult = _mapper.Map<IList<BrandResponse>>(result);
        return Result<IList<BrandResponse>>.Success(mappedResult);
    }
}
