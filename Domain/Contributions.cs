using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MineralWaterMonitoring.Domain;

public class Contributions
{
    [Column("ContributionId")]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid CollectionId { get; set; }
    public Guid PayerId { get; set; }
    public int ContributionAmount { get; set; }
    public virtual Collection Collection { get; set; }
    public virtual Payers Payer { get; set; }
}