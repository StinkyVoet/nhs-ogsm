using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Services;

namespace nhs_ogsmTest;

[TestClass]
public class ActionMeasureServiceTest
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
    public void AddActionTest()
    {
        // Arrange
        var service = new ActionMeasureService(_contextFactory);
        ActionMeasure am1 = new ActionMeasure { Name = "am1", IsDone = false };
        ActionMeasure am2 = new ActionMeasure { Name = "am2", IsDone = false };

        // Act
        service.AddAction(am1);
        service.AddAction(am2);
        var result = GetAllActionMeasure();

        // Assert
        Assert.IsTrue(result.Contains(am1));
        Assert.IsTrue(result.Contains(am2));
    }
    
    [TestMethod]
    public void UpdateActionTest()
    {
        // Arrange
        var service = new ActionMeasureService(_contextFactory);
        ActionMeasure am1 = new ActionMeasure { Name = "am1", IsDone = false };
        service.AddAction(am1);
        am1.Name = "new name";

        // Act
        var result1 = GetAllActionMeasure();
        service.UpdateAction(am1);
        var result2 = GetAllActionMeasure();
        
        // Assert
        Assert.IsFalse(result1[0].Name == am1.Name);
        Assert.IsTrue(result2[0].Name == am1.Name);
    }    
    
    [TestMethod]
    public void DeleteActionTest()
    {
        // Arrange
        var service = new ActionMeasureService(_contextFactory);
        ActionMeasure am1 = new ActionMeasure { Name = "am1", IsDone = false };
        ActionMeasure am2 = new ActionMeasure { Name = "am2", IsDone = false };
        service.AddAction(am1);
        service.AddAction(am2);
        
        // Act
        service.DeleteAction(am1.ID);
        var result = GetAllActionMeasure();
        
        // Assert
        Assert.IsFalse(result.Contains(am1));
        Assert.IsTrue(result.Contains(am2));
    }
    
    private List<ActionMeasure> GetAllActionMeasure()
    {
        using (var context = _contextFactory.CreateDbContext())
        {
            List<ActionMeasure> am = context.Actions.ToList();
            return am;
        }
    }
}