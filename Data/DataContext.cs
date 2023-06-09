using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Groups> Groups { get; set; }
    public virtual DbSet<Payers> Payers { get; set; }
    public virtual DbSet<Collection> Collections { get; set; }
    public virtual DbSet<Contributions> Contributions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contributions>()
            .HasOne(c => c.Payer)
            .WithMany(p => p.Contributions)
            .HasForeignKey(c => c.PayerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    
}