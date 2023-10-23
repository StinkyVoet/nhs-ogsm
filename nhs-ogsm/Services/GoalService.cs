﻿using Microsoft.EntityFrameworkCore;
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
    
    public List<Goal> GetGoals(OgsmItems ogsmItem)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<Goal> ogsmItems = context.ogsm_items.SelectMany(ogsm => ogsm.Goals).Where(ogsm => ogsm.ID == ogsmItem.ID).ToList();
            return ogsmItems;
        }
    }
}