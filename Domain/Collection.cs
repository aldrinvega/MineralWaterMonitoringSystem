using System.ComponentModel.DataAnnotations.Schema;

namespace MineralWaterMonitoring.Domain;

public sealed class Collection
{
    [Column("collection_id")]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [ForeignKey("Groups")]
    public Guid GroupId { get; set; }
    public int CollectionAmount { get; set; }
    public string Status { get; set; }
    public Groups Groups { get; set; }
}