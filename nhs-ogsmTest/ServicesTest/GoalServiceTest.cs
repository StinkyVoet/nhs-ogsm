using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Services;

namespace nhs_ogsmTest;

[TestClass]
public class GoalServiceTest
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
    public void AddGoalTest()
    {
        // Arrange
        
        // Act

        // Assert
       
    }
    
    [TestMethod]
    public void DeleteGoalTest()
    {
        // Arrange
        
        // Act

        // Assert

    }
    
    [TestMethod]
    public void UpdateGoalTest()
    {
        // Arrange
        
        // Act

        // Assert

    }
}