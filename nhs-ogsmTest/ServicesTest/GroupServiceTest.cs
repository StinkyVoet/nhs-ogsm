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
    public void AddUserToGroupTest()
    {
        // Arrange
        var service = new GroupService(_contextFactory);
        Group group1 = new Group() { Name = "s1" };
        
        // Act
        
        // Assert
        
    }
}