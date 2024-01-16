using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Pages;
using Group = System.Text.RegularExpressions.Group;

namespace nhs_ogsm.Services;

public class UserService
{
    private IDbContextFactory<OgsmDbContext> _dbContextFactory;

    public UserService(IDbContextFactory<OgsmDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    private List<User> _users = new List<User>();

    public bool AddUser(User user)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        return true;
    }

    public async Task DeleteUser(int id)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            User entity = context.Users.First(t => t.ID == id);
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public void UpdateUser(User user)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public User GetSingleUser(int userID)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            User user = context.Users.Where(user => user.ID == userID)
                .Include(user => user.Groups)
                .First();

            return user;
        }
    }

    public List<User> GetAllUsers()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            List<User> users = context.Users.Include(u => u.Groups).ToList();
            return users;
        }
    }

    public User? AuthUser(string userEmail, string userPassword)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            User? user = context.Users.Where(user => user.Email == userEmail).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            if (user.Password == userPassword)
            {
                return user;
            }

            return null;

        }
    }

    public void AddGroupToUser(User user, Data.Group selectedgroup)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Data.Group Group = context.Groups.Include(g => g.Users).First(g => g.ID == selectedgroup.ID);
            Data.User User = context.Users.Include(g => g.Groups).First(u => u.ID == user.ID);
            Group.Users.Add(User);
            user.Groups.Add(Group);
            context.SaveChanges();
        }
    }
    
    public void removeGroupFromUser(User user, Data.Group selectedgroup)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            Data.Group Group = context.Groups.Include(g => g.Users).First(g => g.ID == selectedgroup.ID);
            Data.User User = context.Users.Include(g => g.Groups).First(u => u.ID == user.ID);
            Group.Users.Remove(User);
            user.Groups.Remove(Group);
            context.SaveChanges();
        }
    }
}