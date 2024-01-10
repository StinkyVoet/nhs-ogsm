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
    
    public void UpdateGroup(Group group)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Entry(group).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public List<Group> GetAllGroups()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<Group> groups = context.Groups.Include(g => g.Ogsms).ToList();
            return groups;
        }
    }
}