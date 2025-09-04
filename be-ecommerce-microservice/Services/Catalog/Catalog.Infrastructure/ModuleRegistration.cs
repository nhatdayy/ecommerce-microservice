using MediatR;
using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMTECH.BACKEND.PRODUCTS.APPLICATION.Behaviors;
using Catalog.Application.Mappers;

namespace Catalog.Infrastructure;

public static class ModuleRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(AssemblyReference.Assembly, Application.AssemblyReference.Assembly))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);
        services.AddAutoMapper(cfg =>
        {
        }, Application.AssemblyReference.Assembly);
        return services;
    }
}
