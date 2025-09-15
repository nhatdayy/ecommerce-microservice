using Catalog.Core.Abstractions;
using Catalog.Core.Repositories;
using Catalog.Persistence.Data;
using Catalog.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Persistence;

public static class RegisterPersistenceService
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICatalogContext>(sp =>
               new CatalogContext(configuration));
        services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();
        return services;
    }
}
