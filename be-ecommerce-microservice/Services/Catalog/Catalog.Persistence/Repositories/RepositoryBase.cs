using Catalog.Core.Abstractions;
using Contract.Abstarctions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.Persistence.Repositories;

internal class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : AuditableEntity<TKey>
{
    private readonly IMongoCollection<TEntity> _collection;

    public RepositoryBase(IMongoDatabase database, string? collectionName = null)
    {
        _collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = "1";
        entity.IsDeleted = false;
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _collection.FindSync(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<PaginationResult<TEntity>> GetAllAsync(
    PaginationRequest request,
    Expression<Func<TEntity, bool>>? predicate = null)
    {
        var filter = predicate ?? (x => true);

        var totalCount = await _collection.CountDocumentsAsync(filter);

        var items = await _collection
            .Find(filter)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync();

        return new PaginationResult<TEntity>(
            request.PageIndex,
            request.PageSize,
            (int)totalCount,
            items
        );
    }



    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        var entity = await _collection.Find(x => x.Id!.Equals(id) && !x.IsDeleted).FirstOrDefaultAsync();
        if (entity == null)
            throw new KeyNotFoundException($"Entity with Id {id} not found.");
        return entity;
    }

    public async Task<TEntity> RemoveAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        entity.DeletedBy = "1";
        await _collection.ReplaceOneAsync(x => x.Id!.Equals(entity.Id), entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = "1";
        await _collection.ReplaceOneAsync(x => x.Id!.Equals(entity.Id), entity);
        return entity;
    }
}
