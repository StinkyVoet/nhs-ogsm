using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using nhs_ogsm.Pages;

namespace nhs_ogsm.Data;

public class OgsmDbContext : DbContext
{

    public OgsmDbContext(DbContextOptions<OgsmDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseSerialColumns();
        // configures one-to-many relationship
        // Ogsm -> goals
        modelBuilder.Entity<Ogsm>()
                    .HasMany(e => e.Goals)
                    .WithOne(g => g.Ogsm)
                    .HasForeignKey(k => k.ParentOgsmID)
                    .IsRequired();
        modelBuilder.Entity<Goal>()
                    .HasOne(e => e.Ogsm)
                    .WithMany(e => e.Goals)
                    .HasForeignKey(e => e.ParentOgsmID)
                    .IsRequired();
        
        // Goal -> Strategy
        modelBuilder.Entity<Goal>()
                    .HasMany(e => e.Strategies)
                    .WithOne(g => g.ParentGoal)
                    .HasForeignKey(k => k.ParentGoalID)
                    .IsRequired();
    }
    
    public DbSet<Ogsm> Ogsms { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Strategy> Strategies { get; set; }
    // public DbSet<Account> accounts { get; set; }
    //
    // public DbSet<Group> groups { get; set; }

}
