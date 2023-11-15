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
        var ogsmService = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "ogsm1" };
        
        var goalService = new GoalService(_contextFactory);
        Goal goal = new Goal { Name = "adding goal", Description = "random info about goal 1"};

        ogsmService.AddOgsm(ogsm1);
        
        // Act
        goalService.AddGoal(goal);
        
        ogsm1.Goals.Add(goal);
        
        var result = ogsmService.GetSingleOgsm(ogsm1.ID).Goals;

        // Assert
        Assert.IsTrue(result.Contains(goal));
    }
    
    [TestMethod]
    public void DeleteGoalTest()
    {
        // Arrange
        var goalService = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "adding goal1", Description = "random info about goal 1"};
        Goal goal2 = new Goal { Name = "second goal", Description = "random info about goal 2"};
        goalService.AddGoal(goal1);
        goalService.AddGoal(goal2);
        
        // Act
        goalService.DeleteGoal(goal1.ID);
        // var result = goalService.GetAllGoal();

        // Assert
        Assert.IsFalse(result.Contains(goal1));
        Assert.IsTrue(result.Contains(goal2));
    }
}