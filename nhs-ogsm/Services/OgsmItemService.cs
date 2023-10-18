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

    public bool AddOgsmItem(OgsmItems ogsmItems)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.ogsm_items.Add(ogsmItems);
            context.SaveChanges();
        }
        
        return true;
    }
    
    public void DeleteOgsmItem(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            OgsmItems entity = context.ogsm_items.First(t => t.ID == id);
            context.ogsm_items.Remove(entity);
            context.SaveChanges();
        }
    }

    public List<OgsmItems> GetOgsmitems()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<OgsmItems> ogsmItems = context.ogsm_items.ToList();
            return ogsmItems;
        }
    }
}