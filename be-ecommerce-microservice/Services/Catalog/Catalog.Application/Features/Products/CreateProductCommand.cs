using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using FluentValidation;

namespace Catalog.Application.Features.Products;
internal class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên sản phẩm không được để trống.")
            .MaximumLength(100).WithMessage("Tên sản phẩm không được vượt quá 100 ký tự.");

        RuleFor(x => x.Sumary)
            .NotEmpty().WithMessage("Tóm tắt sản phẩm không được để trống.")
            .MaximumLength(250).WithMessage("Tóm tắt sản phẩm không được vượt quá 250 ký tự.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả sản phẩm không được để trống.");

        RuleFor(x => x.ImageFile)
            .NotEmpty().WithMessage("Ảnh sản phẩm là bắt buộc.")
            .Must(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
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

public record CreateProductCommand
(
    string Name,
    string Sumary,
    string Description,
    string ImageFile,
    decimal Price,
    string BrandId,
    string TypeId
) : IQuery<ProductResponse>;
internal class CreateProductCommandHandler : IQueryHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;
    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IBrandRepository brandRepository,
        ITypeRepository typeRepository)
    {
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.BrandId);
        if (brand == null)
            return Result<ProductResponse>.Failure(Error.NotFound);
        var type = await _typeRepository.GetByIdAsync(request.TypeId);
        if (type == null)
            return Result<ProductResponse>.Failure(Error.NotFound);
        var product = await _productRepository.AddAsync(new Product
        {
            Name = request.Name,
            Sumary = request.Sumary,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price,
            Brand = brand,
            Type = type
        });
        var response = _mapper.Map<ProductResponse>(product);
        return Result<ProductResponse>.Success(response);
    }
}