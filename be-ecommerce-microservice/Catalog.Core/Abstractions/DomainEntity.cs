using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Abstractions;

public abstract class DomainEntity<TKey>
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public virtual TKey Id { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public string? DeletedBy { get; set; }
}
