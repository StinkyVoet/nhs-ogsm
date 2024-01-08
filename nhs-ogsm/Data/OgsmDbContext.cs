using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using nhs_ogsm.Pages;

namespace nhs_ogsm.Data;

public class OgsmDbContext : IdentityDbContext
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
        
        // Ogsm -> Strategy
        modelBuilder.Entity<Ogsm>()
                    .HasMany(e => e.Strategies)
                    .WithOne(g => g.Ogsm)
                    .HasForeignKey(k => k.ParentOgsmID)
                    .IsRequired();
        
        // Goal <-> Strategy
        modelBuilder.Entity<Goal>()
                    .HasMany(goal => goal.Strategies)
                    .WithMany(strategy => strategy.Goals);
        
        // Strategy -> Action
        modelBuilder.Entity<Strategy>()
                    .HasMany(strategy => strategy.Actions)
                    .WithOne(action => action.Strategy)
                    .HasForeignKey(action => action.ParentStrategyID)
                    .IsRequired();
        
        // User <-> Action
        modelBuilder.Entity<User>()
                    .HasMany(user => user.Actions)
                    .WithMany(action => action.Users);
        
        // Group <-> Account
        modelBuilder.Entity<Group>()
                    .HasMany(e => e.Users)
                    .WithMany(g => g.Groups);
    }
    
    public DbSet<Ogsm> Ogsms { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Strategy> Strategies { get; set; }
    public DbSet<ActionMeasure> Actions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    
}
