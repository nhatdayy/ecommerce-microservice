using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using FluentValidation;

namespace Catalog.Application.Features.Brands;
internal class  CreateBrandValidatior : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandValidatior()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên thương hiệu không được để trống.")
            .MaximumLength(100).WithMessage("Tên thương hiệu không được vượt quá 100 ký tự.");
    }
}
public record CreateBrandCommand(string Name) : IQuery<BrandResponse>;
internal class CreateBrandCommandhander : IQueryHandler<CreateBrandCommand, BrandResponse>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    public CreateBrandCommandhander(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }
    public async Task<Result<BrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.AddAsync(new ProductBrand
        {
            Name = request.Name,
            IsDeleted = false
        });
        var response = _mapper.Map<BrandResponse>(brand);
        return Result<BrandResponse>.Success(response);
    }
}
