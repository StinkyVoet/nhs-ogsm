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
    
    public bool AddGoal(Goal goal)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Goals.Add(goal);
            context.SaveChanges();
        }
        return true;
    }
    
    public void DeleteGoal(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Goal entity = context.Goals.First(t => t.ID == id);
            context.Goals.Remove(entity);
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
    
    // public List<Goal> GetAllGoal()
    // {
    //     using (var context = _dbContextFactory.CreateDbContext())
    //     {
    //         List<Goal> goals = context.Goals.ToList();
    //         return goals;
    //     }
    // }
    
    // public List<Goal>? GetGoals(OgsmItems ogsmItem)
    // {
    //     using (var context = _dbContextFactory.CreateDbContext())
    //     {
    //         List<Goal>? ogsmItems = context.goals.Where(ogsm => ogsm.ID == ogsmItem.ID).Include(c => c.Goals).FirstOrDefault();
    //         return ogsmItems;
    //     }
    // }
}