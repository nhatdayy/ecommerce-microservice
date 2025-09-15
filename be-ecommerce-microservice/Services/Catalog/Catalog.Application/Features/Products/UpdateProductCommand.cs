using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using FluentValidation;

namespace Catalog.Application.Features.Products;

internal class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id sản phẩm là bắt buộc.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên sản phẩm không được để trống.")
            .MaximumLength(100).WithMessage("Tên sản phẩm không được vượt quá 100 ký tự.");

        RuleFor(x => x.Sumary)
            .NotEmpty().WithMessage("Tóm tắt sản phẩm không được để trống.")
            .MaximumLength(250).WithMessage("Tóm tắt sản phẩm không được vượt quá 250 ký tự.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả sản phẩm không được để trống.");

        RuleFor(x => x.ImageFile)
            .Must(file => string.IsNullOrEmpty(file)
                       || file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                       || file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                       || file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Ảnh phải có định dạng .jpg, .jpeg hoặc .png.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0.");

        RuleFor(x => x.Brand)
            .NotNull().WithMessage("Thương hiệu là bắt buộc.");

        RuleFor(x => x.Type)
            .NotNull().WithMessage("Loại sản phẩm là bắt buộc.");
    }
}
public record UpdateProductCommand
(   string Id,
    string Name,
    string Sumary,
    string Description,
    string ImageFile,
    decimal Price,
    ProductBrand Brand,
    ProductType Type) : IQuery<ProductResponse>;
internal class UpdateProductCommandHandler : IQueryHandler<UpdateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetByIdAsync(request.Id);
        if (existingProduct is null)
        {
            return Result<ProductResponse>.Failure(Error.NotFound);
        }
        existingProduct.Name = request.Name;
        existingProduct.Sumary = request.Sumary;
        existingProduct.Description = request.Description;
        existingProduct.ImageFile = request.ImageFile;
        existingProduct.Price = request.Price;
        existingProduct.Brand = request.Brand;
        existingProduct.Type = request.Type;

        _productRepository.UpdateAsync(existingProduct);

        var productResponse = _mapper.Map<ProductResponse>(existingProduct);
        return Result<ProductResponse>.Success(productResponse);
    }
}
