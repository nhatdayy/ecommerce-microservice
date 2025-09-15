using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Persistence.Data;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories;

internal class BrandRepository : RepositoryBase<ProductBrand, string>, IBrandRepository
{
    public BrandRepository(ICatalogContext context, string? collectionName = null) : base(context.Brands.Database, collectionName)
    {
    }
}
