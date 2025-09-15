using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Core.Repositories;

namespace Catalog.Application.Features.Products;
public record DeleteProductCommand
(string Id): IQuery<bool>;

internal class DeleteProductCommandHandler : IQueryHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var existProduct = await _productRepository.GetByIdAsync(request.Id);
        if (existProduct == null)
        {
            return Result<bool>.Failure(Error.NotFound);
        }
        var result = await _productRepository.RemoveAsync(existProduct);
        if (result.IsDeleted == false)
        {
            return Result<bool>.Failure(Error.NullValue);
        }
        return Result<bool>.Success(true);
    }
}
