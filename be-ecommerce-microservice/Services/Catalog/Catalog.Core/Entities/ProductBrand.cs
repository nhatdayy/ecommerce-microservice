using Catalog.Core.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductBrand : BaseEntity<string>
{
    [BsonElement("Id")]
    [BsonIgnoreIfNull]
    public override string Id { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
}
