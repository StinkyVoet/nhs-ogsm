using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
namespace nhs_ogsm.Services;

public class GoalService
{
    private IDbContextFactory<OgsmDbContext> _dbContextFactory;
    
    public GoalService(IDbContextFactory<OgsmDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public bool AddGoal(Goal goal, OgsmItems ogsmItems)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.goals.Add(goal);
            context.SaveChanges();
        }
        return true;
    }
    
    public void DeleteGoal(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Goal entity = context.goals.First(t => t.ID == id);
            context.goals.Remove(entity);
            context.SaveChanges();
        }
    }
    
    public void UpdateGoal(Goal goal)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(goal).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
    
    // public List<Goal>? GetGoals(OgsmItems ogsmItem)
    // {
    //     using (var context = _dbContextFactory.CreateDbContext())
    //     {
    //         List<Goal>? ogsmItems = context.goals.Where(ogsm => ogsm.ID == ogsmItem.ID).Include(c => c.Goals).FirstOrDefault();
    //         return ogsmItems;
    //     }
    // }
}