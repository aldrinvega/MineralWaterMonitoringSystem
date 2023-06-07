using System.ComponentModel.DataAnnotations.Schema;
using MineralWaterMonitoring.Features.Group;

namespace MineralWaterMonitoring.Domain;

public class Roles
{
    [Column("RolesId")]
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Today;
    public ICollection<Users> Users { get; set; }
}