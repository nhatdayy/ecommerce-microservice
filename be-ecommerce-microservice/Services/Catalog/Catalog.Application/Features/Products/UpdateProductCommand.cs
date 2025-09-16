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

        RuleFor(x => x.BrandId)
            .NotNull().WithMessage("Thương hiệu là bắt buộc.");

        RuleFor(x => x.TypeId)
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
    string BrandId,
    string TypeId) : IQuery<ProductResponse>;
internal class UpdateProductCommandHandler : IQueryHandler<UpdateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly ITypeRepository _typeRepository;
    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ITypeRepository typeRepository,
        IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.BrandId);
        if (brand is null)
        {
            return Result<ProductResponse>.Failure(new Error("Brand.NotFound", $"Không tìm thấy thương hiệu với Id: {request.BrandId}"));
        }
        var type = await _typeRepository.GetByIdAsync(request.TypeId);
        if (type is null)
        {
            return Result<ProductResponse>.Failure(new Error("Type.NotFound", $"Không tìm thấy loại sản phẩm với Id: {request.TypeId}"));
        }
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
        existingProduct.Brand =brand;
        existingProduct.Type = type;

        _productRepository.UpdateAsync(existingProduct);

        var productResponse = _mapper.Map<ProductResponse>(existingProduct);
        return Result<ProductResponse>.Success(productResponse);
    }
}
