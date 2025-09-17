using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Abstractions;

public abstract class BaseEntity<TKey>
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public virtual TKey Id { get; set; }
    public bool IsDeleted { get; set; }
}
