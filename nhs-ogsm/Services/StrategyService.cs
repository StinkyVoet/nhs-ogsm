using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
namespace nhs_ogsm.Services;

public class StrategyService
{
    private IDbContextFactory<OgsmDbContext> _dbContextFactory;
    
    public StrategyService(IDbContextFactory<OgsmDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public void DeleteStrategy(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Strategy entity = context.Strategies.First(t => t.ID == id);
            context.Strategies.Remove(entity);
            context.SaveChanges();
        }
    }
    
    public void UpdateStrategy(Strategy strategy)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(strategy).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
    
    public bool AddStrategy(Strategy strategy)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Strategies.Add(strategy);
            context.SaveChanges();
        }
        return true;
    }

}