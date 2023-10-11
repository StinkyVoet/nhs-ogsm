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
        modelBuilder.UseSerialColumns();
    }

    public DbSet<OgsmItems> ogsm_items { get; set; }
}
