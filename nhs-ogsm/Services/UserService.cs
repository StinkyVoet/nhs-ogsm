using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
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

    public void DeleteUser(int id)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            User entity = context.Users.First(t => t.ID == id);
            context.Users.Remove(entity);
            context.SaveChanges();
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
            List<User> users = context.Users.ToList();
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

    public async Task<IEnumerable<User>> GetUsersAsync(string searchText)
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            // Simulate an asynchronous operation if needed
            await Task.Delay(100); // Simulating a delay

            // Perform the actual data filtering based on the search text
            var filteredUsers = await context.Users
                .Where(user => user.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                               || user.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return filteredUsers;
        }
    }
}