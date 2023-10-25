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

    public bool AddOgsmItem(Ogsm ogsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.ogsm_items.Add(ogsm);
            context.SaveChanges();
        }
        
        return true;
    }
    
    public void DeleteOgsmItem(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Ogsm entity = context.ogsm_items.First(t => t.ID == id);
            context.ogsm_items.Remove(entity);
            context.SaveChanges();
        }
    }

    public List<Ogsm> GetOgsmitems()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<Ogsm> ogsmItems = context.ogsm_items.ToList();
            return ogsmItems;
        }
    }
    
    public Ogsm GetOgsm(int ogsmId)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Ogsm ogsm = context.ogsm_items.Where(o => o.ID.Equals(ogsmId)).Include(c => c.Goals).First();
            return ogsm;
        }
    }


    
    public void UpdateOgsmitems(Ogsm ogsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(ogsm).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}