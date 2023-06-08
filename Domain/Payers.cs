using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MineralWaterMonitoring.Domain;

public class Payers
{
    [Column("PayerId")]
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [ForeignKey("Groups")]
    public Guid GroupId { get; set; }
    public Groups Groups { get; set; }
   
}