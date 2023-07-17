using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MineralWaterMonitoring.Domain;

public sealed class Groups
{
    [Column("group_id")] public Guid Id { get; set; }

    public string GroupCode { get; set; }

    public string GroupName { get; set; }

    [DataType(DataType.Date)] public DateTime DateAdded { get; set; }

    public ICollection<Payers> Payers { get; set; }
    public ICollection<Collection> Collection { get; set; }
}