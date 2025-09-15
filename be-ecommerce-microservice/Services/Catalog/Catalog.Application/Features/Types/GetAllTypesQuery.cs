
using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Types;

public record GetAllTypesQuery
(): IQuery<IList<ProductTypeResponse>>;

internal class GetAllTypesQueryHandler : IQueryHandler<GetAllTypesQuery, IList<ProductTypeResponse>>
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;
    public GetAllTypesQueryHandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _mapper = mapper;
        _typeRepository = typeRepository;
    }
    public async Task<Result<IList<ProductTypeResponse>>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _typeRepository.GetAllAsync();
        var typeResponses = _mapper.Map<IList<ProductTypeResponse>>(result);
        return Result<IList<ProductTypeResponse>>.Success(typeResponses);

    }
}
