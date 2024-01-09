using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Services;

namespace nhs_ogsmTest;

[TestClass]
public class UserServiceTest
{
    private IDbContextFactory<OgsmDbContext> _contextFactory = null!;
    
    [TestInitialize]
    public void Setup()
    {
        _contextFactory = new TestFactory();
    }
    
    [TestCleanup]
    public void CleanUp()
    {
        TestFactory.CleanDbContext();
    }

    [TestMethod]
    public void AddUserTest()
    {
        // Arrange
        var service = new UserService(_contextFactory);
        User user1 = new User { Email = "user1@test.test", FirstName = "u", LastName = "1", Password = "password" };

        // Act
        service.AddUser(user1);
        var result = service.GetSingleUser(user1.ID);

        // Assert
        Assert.IsTrue(result.Equals(user1));
    }
    
    [TestMethod]
    public void DeleteUserTest()
    {
        // Arrange
        var service = new UserService(_contextFactory);
        User user1 = new User { Email = "user1@test.test", FirstName = "u1", LastName = "1", Password = "password" };
        User user2 = new User { Email = "user2@test.test", FirstName = "u1", LastName = "2", Password = "password" };
        
        service.AddUser(user1);
        service.AddUser(user2);

        // Act
        service.DeleteUser(user1.ID);
        var result = GetAllUser();

        // Assert
        Assert.IsFalse(result.Contains(user1));
        Assert.IsTrue(result.Contains(user2));
    }
    
    [TestMethod]
    public void UpdateUser()
    {
        // Arrange
        var service = new UserService(_contextFactory);
        User user1 = new User { Email = "user1@test.test", FirstName = "u1", LastName = "1", Password = "password" };
        service.AddUser(user1);
        user1.FirstName = "new name";
        
        // Act
        var result1 = service.GetSingleUser(user1.ID);
        service.UpdateUser(user1);
        var result2 = service.GetSingleUser(user1.ID);
        
        // Assert
        Assert.IsFalse(result1.FirstName == user1.FirstName);
        Assert.IsTrue(result2.FirstName == user1.FirstName);
    }

    [TestMethod]
    public void GetSingleUserTest()
    {
        // Arrange
        var service = new UserService(_contextFactory);
        User user1 = new User { Email = "user1@test.test", FirstName = "u1", LastName = "1", Password = "password" };
        User user2 = new User { Email = "user2@test.test", FirstName = "u1", LastName = "2", Password = "password" };
        
        service.AddUser(user1);
        service.AddUser(user2);

        // Act
        var result = service.GetSingleUser(user2.ID);

        // Assert
        Assert.IsTrue(result.Equals(user2));
    }

    [TestMethod]
    public void AuthUserTest()
    {
        // Arrange
        var service = new UserService(_contextFactory);
        User user1 = new User { Email = "user1@test.test", FirstName = "u1", LastName = "1", Password = "password" };    
        
        service.AddUser(user1);

        // Act
        var result1 = service.AuthUser("user1@test.test", "wrong");
        var result2 = service.AuthUser("user1@test.test", "password");

        // Assert
        Assert.IsNull(result1);
        Assert.AreEqual(result2, user1);
    }
    
    private List<User> GetAllUser()
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            List<User> users = context.Users.ToList();
            return users;
        }
    }
}