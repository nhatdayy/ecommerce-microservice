using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories;

internal class BrandRepository : RepositoryBase<ProductBrand, string>, IBrandRepository
{
    public BrandRepository(IMongoDatabase database, string? collectionName = null) : base(database, collectionName)
    {
    }
}
