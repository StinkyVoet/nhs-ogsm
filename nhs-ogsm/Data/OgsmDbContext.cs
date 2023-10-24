using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using nhs_ogsm.Pages;

namespace nhs_ogsm.Data;

public class OgsmDbContext : DbContext
{

    public OgsmDbContext(DbContextOptions<OgsmDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseSerialColumns();
        // configures one-to-many relationship
        modelBuilder.Entity<OgsmItems>()
                    .HasMany(e => e.Goals)
                    .WithOne(g => g.OgsmItem)
                    .HasForeignKey(k => k.OgsmItemID)
                    .IsRequired();
        modelBuilder.Entity<Goal>()
                    .HasOne(e => e.OgsmItem)
                    .WithMany(e => e.Goals)
                    .HasForeignKey(e => e.OgsmItemID)
                    .IsRequired();
    }
    
    public DbSet<OgsmItems> ogsm_items { get; set; }
    public DbSet<Goal> goals { get; set; }
    // public DbSet<Account> accounts { get; set; }
    //
    // public DbSet<Group> groups { get; set; }

}
