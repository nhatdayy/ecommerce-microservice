using Catalog.Core.Abstractions;
using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductRepository : IRepositoryBase<Product, string>
{
    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
}
