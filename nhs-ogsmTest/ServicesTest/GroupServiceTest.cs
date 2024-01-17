using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Services;

namespace nhs_ogsmTest;

[TestClass]
public class GroupServiceTest
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
    public void AddGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        Group group2 = new Group() { Name = "g2" };

        // Act
        service.AddGroup(group1);
        service.AddGroup(group2);
        var result = service.GetAllGroups();

        // Assert
        Assert.IsTrue(result.Contains(group1));
        Assert.IsTrue(result.Contains(group2));
    }    
    
    [TestMethod]
    public void UpdateGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        service.AddGroup(group1);
        group1.Name = "new name";
        
        // Act
        var result1 = service.GetSingleGroup(group1.ID);
        service.UpdateGroup(group1);
        var result2 = service.GetSingleGroup(group1.ID);

        // Assert
        Assert.IsFalse(result1.Name == group1.Name);
        Assert.IsTrue(result2.Name == group1.Name);
    }    
    
    [TestMethod]
    public void DeleteGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        Group group2 = new Group() { Name = "g2" };
        service.AddGroup(group1);
        service.AddGroup(group2);
        
        // Act
        service.DeleteGroup(group1.ID);
        var result = service.GetAllGroups();

        // Assert
        Assert.IsFalse(result.Contains(group1));
        Assert.IsTrue(result.Contains(group2));
    }
    
    [TestMethod]
    public void GetAllGroupsTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        
        // Act
        var result = service.GetAllGroups();

        // Assert
        Assert.IsTrue(result.Count == 0);
    }
    
    [TestMethod]
    public void GetSingleGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        Group group2 = new Group() { Name = "g2" };
        service.AddGroup(group1);
        service.AddGroup(group2);
        
        // Act
        var result = service.GetSingleGroup(group2.ID);

        // Assert
        Assert.AreEqual(group2, result);
    }
    
    [TestMethod]
    public void AddUserToGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        service.AddGroup(group1);
        
        User user1 = new User { Email = "user1@test.test", FirstName = "u1", LastName = "1", Password = "password" };
        User user2 = new User { Email = "user2@test.test", FirstName = "u1", LastName = "2", Password = "password" };
        new UserService(_contextFactory).AddUser(user1);
        new UserService(_contextFactory).AddUser(user2);
        
        // Act
        service.AddUserToGroup(user1, group1);
        service.AddUserToGroup(user2, group1);
        var result = service.GetSingleGroup(group1.ID).Users;
        
        // Assert
        Assert.IsTrue(result.Contains(user1));
        Assert.IsTrue(result.Contains(user2));
    }    
    
    [TestMethod]
    public void RemoveUserFromGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        service.AddGroup(group1);
        
        User user1 = new User { Email = "user1@test.test", FirstName = "u1", LastName = "1", Password = "password" };
        User user2 = new User { Email = "user2@test.test", FirstName = "u1", LastName = "2", Password = "password" };
        new UserService(_contextFactory).AddUser(user1);
        new UserService(_contextFactory).AddUser(user2);
        
        service.AddUserToGroup(user1, group1);
        service.AddUserToGroup(user2, group1);
        
        // Act
        service.RemoveUserFromGroup(user1, group1);
        var result = service.GetSingleGroup(group1.ID).Users;
        
        // Assert
        Assert.IsFalse(result.Contains(user1));
        Assert.IsTrue(result.Contains(user2));
    }  
    
    [TestMethod]
    public void AddOgsmToGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        service.AddGroup(group1);
        
        Ogsm ogsm1 = new Ogsm { Title = "o1" };
        Ogsm ogsm2 = new Ogsm { Title = "o2" };
        new OgsmItemService(_contextFactory).AddOgsm(ogsm1);
        new OgsmItemService(_contextFactory).AddOgsm(ogsm2);
        
        // Act
        service.AddOgsmToGroup(group1, ogsm1);
        service.AddOgsmToGroup(group1, ogsm2);
        var result = service.GetSingleGroup(group1.ID).Ogsms;
        
        // Assert
        Assert.IsTrue(result.Contains(ogsm1));
        Assert.IsTrue(result.Contains(ogsm2));
    }
    
    [TestMethod]
    public void RemoveOgsmFromGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "g1" };
        service.AddGroup(group1);
        
        Ogsm ogsm1 = new Ogsm { Title = "o1" };
        Ogsm ogsm2 = new Ogsm { Title = "o2" };
        new OgsmItemService(_contextFactory).AddOgsm(ogsm1);
        new OgsmItemService(_contextFactory).AddOgsm(ogsm2);
        
        service.AddOgsmToGroup(group1, ogsm1);
        service.AddOgsmToGroup(group1, ogsm2);
        
        // Act
        service.RemoveOgsmFromGroup(group1, ogsm1);
        var result = service.GetSingleGroup(group1.ID).Ogsms;
        
        // Assert
        Assert.IsFalse(result.Contains(ogsm1));
        Assert.IsTrue(result.Contains(ogsm2));
    }
}