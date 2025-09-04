using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories;

internal class TypeRepository : RepositoryBase<ProductType, string>, ITypeRepository
{
    public TypeRepository(IMongoDatabase database, string? collectionName = null) : base(database, collectionName)
    {
    }
}
