using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Persistence.Data;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories;

internal class TypeRepository : RepositoryBase<ProductType, string>, ITypeRepository
{
    public TypeRepository(ICatalogContext context, string? collectionName = null) : base(context.Types.Database, collectionName)
    {
    }
}
