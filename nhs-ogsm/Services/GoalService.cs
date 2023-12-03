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
    
    public Goal GetSingleGoal(int goalID)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Goal goal = context.Goals.First(g => g.ID == goalID);
            return goal;
        }
    }
    
    
    public bool AddStratToGoal(Strategy strategy, Goal goal)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Goal parentGoal = context.Goals.Include(g => g.Strategies).First(g => g.ID == goal.ID);
            if (strategy.ID != 0) strategy = context.Strategies.First(strategy1 => strategy1.ID == strategy.ID);
            if (parentGoal.Strategies == null) return false;
            parentGoal.Strategies.Add(strategy);
            context.SaveChanges();
        }
        return true;
    }
    
    
    public bool RemoveStratFromGoal(Strategy strategy, Goal goal)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Goal goalFromDb = context.Goals.Include(g => g.Strategies).First(g => g.ID == goal.ID);
            goalFromDb.Strategies.Remove(goalFromDb.Strategies.First(s => s.ID == strategy.ID));
            context.SaveChanges();
        }

        return true;
    }
}