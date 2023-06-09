using System.ComponentModel.DataAnnotations.Schema;

namespace MineralWaterMonitoring.Domain;

public class Collection
{
    [Column("CollectionId")]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    [ForeignKey("Groups")]
    public Guid GroupId { get; set; }
    public int CollectionAmount { get; set; }
    public virtual Groups Groups { get; set; }
}