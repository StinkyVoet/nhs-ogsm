using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Services;

namespace nhs_ogsmTest;

[TestClass]
public class StrategyServiceTest
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
    public void AddStrategyTest()
    {
        // Arrange
        var service = new StrategyService(_contextFactory);
        Strategy strategy1 = new Strategy { Name = "s1" };
        
        // Act
        service.AddStrategy(strategy1);
        var result = GetAllStrategy(); 
        
        // Assert
        Assert.IsTrue(result.Contains(strategy1));
    }
    
    [TestMethod]
    public void UpdateStrategy()
    {
        // Arrange
        var service = new StrategyService(_contextFactory);
        Strategy strategy1 = new Strategy { Name = "s1" };
        service.AddStrategy(strategy1);
        strategy1.Name = "new name";
        
        // Act
        var result1 = GetAllStrategy(); 
        service.UpdateStrategy(strategy1); 
        var result2 = GetAllStrategy(); 
        
        // Assert
        Assert.IsFalse(result1.Contains(strategy1));
        Assert.IsTrue(result2.Contains(strategy1));
    }
    
    [TestMethod]
    public void DeleteStrategy()
    {
        // Arrange
        var service = new StrategyService(_contextFactory);
        Strategy strategy1 = new Strategy { Name = "s1" };
        Strategy strategy2 = new Strategy { Name = "s2" };
        service.AddStrategy(strategy1);
        service.AddStrategy(strategy2);
        
        // Act
        service.DeleteStrategy(strategy1.ID); 
        var result = GetAllStrategy(); 
        
        // Assert
        Assert.IsFalse(result.Contains(strategy1));
        Assert.IsTrue(result.Contains(strategy2));
    }

    private List<Strategy> GetAllStrategy()
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            List<Strategy> strategies = context.Strategies.ToList();
            return strategies;
        }   
    }
}