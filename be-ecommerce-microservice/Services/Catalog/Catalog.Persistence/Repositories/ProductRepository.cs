using Catalog.Core.Abstractions;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Contract.Abstarctions;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories;
internal class ProductRepository : RepositoryBase<Product, string>, IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IMongoDatabase database, string? collectionName = null) : base(database, collectionName)
    {
        _collection = database.GetCollection<Product>(collectionName ?? typeof(Product).Name);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        return await _collection.Aggregate()
            .Match(p => p.Brand != null && p.Brand.Name == brandName && !p.IsDeleted)
            .ToListAsync();
    }
}
