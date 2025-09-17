using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Types;

public record GetTypeByIdQuery
(string Id): IQuery<ProductTypeResponse>;

internal class GetTypeByIdQueryHandler : IQueryHandler<GetTypeByIdQuery, ProductTypeResponse>
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public GetTypeByIdQueryHandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _mapper = mapper;
        _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
    }

    public async Task<Result<ProductTypeResponse>> Handle(GetTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetByIdAsync(request.Id);
        if (type == null) return null;
        var result = _mapper.Map<ProductTypeResponse>(type);
        return Result<ProductTypeResponse>.Success(result);
    }
}
