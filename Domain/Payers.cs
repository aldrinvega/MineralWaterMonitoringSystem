using System.ComponentModel.DataAnnotations.Schema;

namespace MineralWaterMonitoring.Domain;

public sealed class Payers
{
    [Column("payer_id")] public Guid Id { get; set; }
    public string Fullname { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [ForeignKey("Groups")] public Guid GroupId { get; set; }
    public int Balance { get; set; }
    public Groups Groups { get; set; }
    public ICollection<Contributions> Contributions { get; set; }
}