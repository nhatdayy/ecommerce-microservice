using Catalog.Core.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductBrand : AuditableEntity<string>
{
    [BsonElement("Name")]
    public string Name { get; set; }
}
