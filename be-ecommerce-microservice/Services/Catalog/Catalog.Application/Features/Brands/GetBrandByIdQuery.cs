using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Brands;

public record GetBrandByIdQuery (string Id) : IQuery<BrandResponse>;

internal class GetBrandByIdQueryHandler : IQueryHandler<GetBrandByIdQuery, BrandResponse>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<Result<BrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id);
        if (brand == null)
            return Result<BrandResponse>.Failure(Error.NotFound);
        var brandResponse = _mapper.Map<BrandResponse>(brand);
        return Result<BrandResponse>.Success(brandResponse);
    }
}
