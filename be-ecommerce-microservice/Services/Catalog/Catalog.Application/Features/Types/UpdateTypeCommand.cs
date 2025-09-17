using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Types;
public record UpdateTypeCommand
(
    string Id,
    string Name
) : IQuery<ProductTypeResponse>;
internal class UpdateTypeCommandHandler : IQueryHandler<UpdateTypeCommand, ProductTypeResponse>
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;
    public UpdateTypeCommandHandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _mapper = mapper;
        _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
    }
    public async Task<Result<ProductTypeResponse>> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        var existingType = await _typeRepository.GetByIdAsync(request.Id);
        if (existingType != null) {
            existingType.Name = request.Name;
            var updatedType = await _typeRepository.UpdateAsync(existingType);
            var response = _mapper.Map<ProductTypeResponse>(updatedType);
            return Result<ProductTypeResponse>.Success(response);
        }
        return Result<ProductTypeResponse>.Failure(Error.NotFound);
    }
}
