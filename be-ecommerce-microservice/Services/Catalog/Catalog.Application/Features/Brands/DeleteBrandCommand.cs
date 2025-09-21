using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Brands;
public record DeleteBrandCommand
(string Id): IQuery<bool>;

internal class DeleteBrandCommandHandler : IQueryHandler<DeleteBrandCommand, bool>
{
    private readonly IBrandRepository _brandRepository;
    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
    }
    public async Task<Result<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var existingBrand = await _brandRepository.GetByIdAsync(request.Id);
        if (existingBrand != null) {
            await _brandRepository.RemoveAsync(existingBrand);
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure(Error.NotFound);
    }
}
