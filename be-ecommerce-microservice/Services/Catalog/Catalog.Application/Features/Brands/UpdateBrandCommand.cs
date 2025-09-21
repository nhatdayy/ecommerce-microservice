using AutoMapper;
using Catalog.Contracts.Abstractions.Message;
using Catalog.Contracts.Abstractions.Shared;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Repositories;
using FluentValidation;

namespace Catalog.Application.Features.Brands;
internal class UpdateBrandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id thương hiệu không được để trống.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên thương hiệu không được để trống.")
            .MaximumLength(100).WithMessage("Tên thương hiệu không được vượt quá 100 ký tự.");
    }
}
public record UpdateBrandCommand
(
    string Id,
    string Name
): IQuery<BrandResponse>;
internal class UpdateBrandCommandhandler : IQueryHandler<UpdateBrandCommand, BrandResponse>
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;
    public UpdateBrandCommandhandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
    }
    public Task<Result<BrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var existingBrand = _typeRepository.GetByIdAsync(request.Id);
        if (existingBrand != null) {
            existingBrand.Result.Name = request.Name;
            var updatedBrand = _typeRepository.UpdateAsync(existingBrand.Result);
            var response = _mapper.Map<BrandResponse>(updatedBrand.Result);
            return Task.FromResult(Result<BrandResponse>.Success(response));
        }
        return Task.FromResult(Result<BrandResponse>.Failure(Error.NotFound));
    }
}
