namespace Catalog.Core.Abstractions;

public interface IAuditableEntity
{
    DateTime? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}

public class AuditableEntity<Tkey> : DomainEntity<Tkey>, IAuditableEntity
{
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
