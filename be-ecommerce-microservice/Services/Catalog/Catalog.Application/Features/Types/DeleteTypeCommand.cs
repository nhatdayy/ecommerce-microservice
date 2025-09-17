using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Types;
public record DeleteTypeCommand(string Id) : IQuery<bool>;
internal class DeleteTypeCommandHandler : IQueryHandler<DeleteTypeCommand, bool>
{
    private readonly ITypeRepository _typeRepository;
    public DeleteTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
    }
    public async Task<Result<bool>> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        var existingType = await _typeRepository.GetByIdAsync(request.Id);
        if (existingType != null)
        {
            await _typeRepository.RemoveAsync(existingType);
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure(Error.NotFound);
    }
}
