using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
namespace nhs_ogsm.Services;

public class GroupService
{
    private IDbContextFactory<OgsmDbContext> _dbContextFactory;
    
    public GroupService(IDbContextFactory<OgsmDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public bool AddUserToGroup(User account, Group group)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            group.Users.Add(account); 
            context.SaveChanges();
            UpdateGroup(group);
        }
        return true;
    }

    public bool RemoveUserFromGroup(User account, Group group)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            group.Users.Remove(account);
            UpdateGroup(group);
        }
        return true;
    }
    
    public void AddOgsmToGroup(Group group, Data.Ogsm selectedOgsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Data.Ogsm Ogsm = context.Ogsms.Include(o => o.Groups).First(o => o.ID == selectedOgsm.ID);
            Data.Group Group = context.Groups.Include(g => g.Ogsms).First(g => g.ID == group.ID);
            Ogsm.Groups.Add(Group);
            group.Ogsms.Add(Ogsm);
            context.SaveChanges();
        }
    }
    
    public void RemoveOgsmFromGroup(Group group, Data.Ogsm selectedOgsm)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Data.Ogsm Ogsm = context.Ogsms.Include(o => o.Groups).First(o => o.ID == selectedOgsm.ID);
            Data.Group Group = context.Groups.Include(g => g.Ogsms).First(g => g.ID == group.ID);
            Ogsm.Groups.Remove(Group);
            group.Ogsms.Remove(Ogsm);
            context.SaveChanges();
        }
    }
    
    public void UpdateGroup(Group group)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(group).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
    
    public bool AddGroup(Group group)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Groups.Add(group);
            context.SaveChanges();
        }
        
        return true;
    }
    
    public void DeleteGroup(int id)  
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Group entity = context.Groups.First(t => t.ID == id);
            context.Groups.Remove(entity);
            context.SaveChanges();
        }
    }

    public List<Group> GetAllGroups()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<Group> groups = context.Groups.ToList();
            return groups;
        }
    }
    
    public Group GetSingleGroup(int groupID)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Group group = context.Groups.Include(g => g.Users)
                .Where(group => group.ID == groupID)
                .First();

            return group;
        }
    }
}
