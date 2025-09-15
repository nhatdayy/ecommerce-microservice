
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
    public GetAllTypesQueryHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }
    public async Task<Result<IList<ProductTypeResponse>>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _typeRepository.GetAllAsync();
        IList<ProductTypeResponse> typeResponses = result.Select(t => new ProductTypeResponse
        {
            Id = t.Id,
            Name = t.Name
        }).ToList();
        return Result.Success(typeResponses);
    }
}
