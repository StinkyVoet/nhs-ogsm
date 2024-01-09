using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;

namespace nhs_ogsm.Services;

public class OgsmItemService
{
    private IDbContextFactory<OgsmDbContext> _dbContextFactory;

    public OgsmItemService(IDbContextFactory<OgsmDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public bool AddOgsm(Ogsm ogsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Ogsms.Add(ogsm);
            context.SaveChanges();
        }
        
        return true;
    }
    
    public void DeleteOgsm(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Ogsm entity = context.Ogsms.First(t => t.ID == id);
            context.Ogsms.Remove(entity);
            context.SaveChanges();
        }
    }

    public List<Ogsm> GetAllOgsm()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<Ogsm> ogsmItems = context.Ogsms.Include(ogsm => ogsm.Goals).Include(ogsm => ogsm.Strategies).ThenInclude(strategy => strategy.Actions).ToList();
            return ogsmItems;
        }
    }
    
    public Ogsm GetSingleOgsm(int ogsmId)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Ogsm ogsm = context.Ogsms.Where(ogsm => ogsm.ID == ogsmId)
                .Include(c => c.Goals).ThenInclude(c => c.Strategies).ThenInclude(c => c.Actions)
                .Include(c => c.Strategies).ThenInclude(c => c.Actions).First();
            return ogsm;
        }
    }

    public List<Ogsm> GetOgsmsHierarchy()
    {
        using var context = _dbContextFactory.CreateDbContext();
        return context.Ogsms.Where(o => o.Parent == null).ToList();
    }
    
    public void UpdateOgsm(Ogsm ogsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(ogsm).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public List<Ogsm> LoadChildren(Ogsm ogsm)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Add(ogsm);
        return context.Entry(ogsm)
            .Collection(o => o.Children)
            .Query()
            .ToList();
    }
}