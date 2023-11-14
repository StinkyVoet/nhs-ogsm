using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;

namespace nhs_ogsmTest;

public class TestFactory : IDbContextFactory<OgsmDbContext>
{
    public OgsmDbContext CreateDbContext()
    {
        DbContextOptions<OgsmDbContext> options;
        var builder = new DbContextOptionsBuilder<OgsmDbContext>();
        builder.UseInMemoryDatabase("TestDatabase");
        options = builder.Options;
        
        return new OgsmDbContext(options);
    }
}