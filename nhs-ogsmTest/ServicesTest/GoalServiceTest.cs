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
        var service = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "g1", EndDate = DateTimeOffset.Now };

        // Act
        service.AddGoal(goal1);
        var result = service.GetSingleGoal(goal1.ID);

        // Assert
        Assert.IsTrue(result.Equals(goal1));
    }
    
    [TestMethod]
    public void DeleteGoalTest()
    {
        // Arrange
        var service = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "g1", EndDate = DateTimeOffset.Now };
        Goal goal2 = new Goal { Name = "g2", EndDate = DateTimeOffset.Now };
        
        service.AddGoal(goal1);
        service.AddGoal(goal2);
        
        // Act
        service.DeleteGoal(goal1.ID);
        var result = GetAllGoal();

        // Assert
        Assert.IsFalse(result.Contains(goal1));
        Assert.IsTrue(result.Contains(goal2));
    }
    
    [TestMethod]
    public void UpdateGoalTest()
    {
        // Arrange
        var service = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "g1", EndDate = DateTimeOffset.Now };
        service.AddGoal(goal1);
        goal1.Name = "new name";
        
        // Act
        var result1 = service.GetSingleGoal(goal1.ID);
        service.UpdateGoal(goal1);
        var result2 = service.GetSingleGoal(goal1.ID);
        
        // Assert
        Assert.IsFalse(result1.Name == goal1.Name);
        Assert.IsTrue(result2.Name == goal1.Name);
    }

    [TestMethod]
    public void GetSingleGoalTest()
    {
        // Arrange
        var service = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "g1", EndDate = DateTimeOffset.Now };
        Goal goal2 = new Goal { Name = "g2", EndDate = DateTimeOffset.Now };
        service.AddGoal(goal1);
        service.AddGoal(goal2);

        // Act
        var result = service.GetSingleGoal(goal2.ID);

        // Assert
        Assert.IsTrue(result.Equals(goal2));
    }
    
    [TestMethod]
    public void AddStratToGoalTest()
    {
        // Arrange
        var service = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "g1", EndDate = DateTimeOffset.Now };
        service.AddGoal(goal1);

        Strategy strategy1 = new Strategy { Name = "s1" };
        
        // Act
        service.AddStratToGoal(strategy1, goal1);
        var result = service.GetSingleGoal(goal1.ID).Strategies;
        
        // Assert
        Assert.IsTrue(result.Contains(strategy1));
    }    
    
    [TestMethod]
    public void RemoveStratFromGoalTest()
    {
        // Arrange
        var service = new GoalService(_contextFactory);
        Goal goal1 = new Goal { Name = "g1", EndDate = DateTimeOffset.Now };
        service.AddGoal(goal1);

        Strategy strategy1 = new Strategy { Name = "s1" };
        Strategy strategy2 = new Strategy { Name = "s2" };
        service.AddStratToGoal(strategy1, goal1);
        service.AddStratToGoal(strategy2, goal1);
        
        // Act
        service.RemoveStratFromGoal(strategy1, goal1);
        var result = service.GetSingleGoal(goal1.ID).Strategies;
        
        // Assert
        Assert.IsFalse(result.Contains(strategy1));
        Assert.IsTrue(result.Contains(strategy2));
    }
    
    private List<Goal> GetAllGoal()
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            List<Goal> goals = context.Goals.Include(goal => goal.Strategies).ThenInclude(strategy => strategy.Actions).ToList();
            return goals;
        }
    }
}