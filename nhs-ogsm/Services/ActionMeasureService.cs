using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
namespace nhs_ogsm.Services;

public class ActionMeasureService
{
    private IDbContextFactory<OgsmDbContext> _dbContextFactory;
    
    public ActionMeasureService(IDbContextFactory<OgsmDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    
    public void DeleteAction(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            ActionMeasure entity = context.Actions.First(t => t.ID == id);
            context.Actions.Remove(entity);
            context.SaveChanges();
        }
    }
    
    public void UpdateAction(ActionMeasure actionMeasure)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(actionMeasure).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
    
    public bool AddAction(ActionMeasure actionMeasure)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Actions.Add(actionMeasure);
            context.SaveChanges();
        }
        return true;
    }
}