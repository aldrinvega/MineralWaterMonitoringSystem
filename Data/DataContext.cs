using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Groups> Groups { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }
}