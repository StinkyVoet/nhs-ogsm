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
            List<Ogsm> ogsmItems = context.Ogsms.Include(ogsm => ogsm.Goals).ThenInclude(goal => goal.Strategies).ToList();
            return ogsmItems;
        }
    }
    
    public Ogsm GetSingleOgsm(int ogsmId)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Ogsm ogsm = context.Ogsms.Where(ogsm => ogsm.ID == ogsmId).Include(c => c.Goals).ThenInclude(c => c.Strategies).Include(c => c.Strategies).ThenInclude(c => c.Goals).First();
            return ogsm;
        }
    }


    
    public void UpdateOgsm(Ogsm ogsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(ogsm).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}