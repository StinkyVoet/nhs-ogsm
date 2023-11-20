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
    
    public bool AddGoal(Goal goal, Ogsm ogsm)
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
    
    
    public bool AddStratToGoal(Strategy strategy, Goal goal)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Goal parentGoal = context.Goals.Include(goal1 => goal1.Strategies).First(goal1 => goal1.ID == goal.ID);
            if (parentGoal.Strategies != null) parentGoal.Strategies.Add(strategy);
            context.SaveChanges();
        }
        return true;
    }
}