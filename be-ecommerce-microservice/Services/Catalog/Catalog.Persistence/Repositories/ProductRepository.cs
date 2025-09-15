using Catalog.Core.Abstractions;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Persistence.Data;
using Contract.Abstarctions;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories;
internal class ProductRepository : RepositoryBase<Product, string>, IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(ICatalogContext context, string? collectionName = null)
        : base(context.Products.Database, collectionName)
    {
        _collection = context.Products; 
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        return await _collection.Aggregate()
            .Match(p => p.Brand != null && p.Brand.Name == brandName && !p.IsDeleted)
            .ToListAsync();
    }
}
