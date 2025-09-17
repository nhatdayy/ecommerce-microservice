using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Types;
public record CreateTypeCommand
(
    string Name
): IQuery<ProductTypeResponse>;
internal class CreateTypeCommandHandler : IQueryHandler<CreateTypeCommand, ProductTypeResponse>
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;
    public CreateTypeCommandHandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _mapper = mapper;
        _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
    }
    public async Task<Result<ProductTypeResponse>> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.AddAsync(new ProductType
        {
            Name = request.Name,
            IsDeleted = false
        });
        var response = _mapper.Map<ProductTypeResponse>(type);
        return Result<ProductTypeResponse>.Success(response);
    }
}
