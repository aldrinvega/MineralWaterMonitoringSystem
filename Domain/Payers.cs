using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MineralWaterMonitoring.Domain;

public class Payers
{
    [Column("PayerId")] public Guid Id { get; set; }
    public string Fullname { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [ForeignKey("Groups")] public Guid GroupId { get; set; }

    public int Balance { get; set; }
    public virtual Groups Groups { get; set; }
    public virtual ICollection<Contributions> Contributions { get; set; }
}