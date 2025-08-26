using Contract.Abstarctions;

namespace Catalog.Core.Abstractions;
public interface IRepositoryBase<TEntity, in Tkey> where TEntity : DomainEntity<Tkey>
{
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(Tkey id);
    Task<TEntity> RemoveAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync(PaginationRequest request);
}
