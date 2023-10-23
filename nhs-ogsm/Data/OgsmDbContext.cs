using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        // modelBuilder.Entity<Goal>()
        //             .HasOne<OgsmItems>()
        //             .WithMany(g => g.Goals)
        //             .HasForeignKey(k => k.ID);
        
        // modelBuilder.Entity<OgsmItems>()
        //     .HasOne<OgsmItems>()
        //     .WithMany(g => g.Children)
        //     .HasForeignKey(k => k.ID);
    }
    
    public DbSet<OgsmItems> ogsm_items { get; set; }
    public DbSet<Account> accounts { get; set; }
    public DbSet<Goal> goals { get; set; }
    public DbSet<Group> groups { get; set; }
}
