using System.ComponentModel.DataAnnotations.Schema;

namespace MineralWaterMonitoring.Domain;

public sealed class Contributions
{
    [Column("contribution_id")]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid CollectionId { get; set; }
    public Guid PayerId { get; set; }
    public int ContributionAmount { get; set; }
    public Collection Collection { get; set; }
    public Payers Payer { get; set; }
}