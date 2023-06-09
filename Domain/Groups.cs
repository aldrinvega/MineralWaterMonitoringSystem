using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MineralWaterMonitoring.Domain;

public class Groups
{
    [Column("GroupId")] public Guid Id { get; set; }

    public string GroupCode { get; set; }

    public string GroupName { get; set; }

    [DataType(DataType.Date)] public DateTime DateAdded { get; set; }

    public virtual ICollection<Payers> Payers { get; set; }
    public virtual ICollection<Collection> Collection { get; set; }
}