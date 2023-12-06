using Microsoft.EntityFrameworkCore;
using nhs_ogsm.Data;
using nhs_ogsm.Services;

namespace nhs_ogsmTest;

[TestClass]
public class OgsmItemServiceTests
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
    public void AddOgsmTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "Testing AddOgsm" };
        Ogsm ogsm2 = new Ogsm { Title = "Second AddOgsm test" };
        
        // Act
        service.AddOgsm(ogsm1);
        service.AddOgsm(ogsm2);
        var result = service.GetAllOgsm(); 
        
        // Assert
        Assert.IsTrue(result.Contains(ogsm1));
        Assert.IsTrue(result.Contains(ogsm2));
    }
    
    [TestMethod]
    public void DeleteOgsmTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "Testing AddOgsm" };
        Ogsm ogsm2 = new Ogsm { Title = "Second AddOgsm test" };
        service.AddOgsm(ogsm1);
        service.AddOgsm(ogsm2);
        
        // Act
        service.DeleteOgsm(ogsm1.ID);
        var result = service.GetAllOgsm(); 
        
        // Assert
        Assert.IsFalse(result.Contains(ogsm1));
        Assert.IsTrue(result.Contains(ogsm2));
    }    
    
    [TestMethod]
    public void GetAllOgsmTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        
        // Act
        var result = service.GetAllOgsm(); 
        
        // Assert
        Assert.IsTrue(result.Count == 0);
    }
    
    [TestMethod]
    public void GetSingleOgsmTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "Testing AddOgsm" };
        Ogsm ogsm2 = new Ogsm { Title = "Second AddOgsm test" };
        service.AddOgsm(ogsm1);
        service.AddOgsm(ogsm2);
        
        // Act
        var result = service.GetSingleOgsm(ogsm2.ID); 
        
        // Assert
        Assert.IsTrue(result.Equals(ogsm2));
    }

    [TestMethod]
    public void UpdateOgsmTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "Testing AddOgsm" };
        service.AddOgsm(ogsm1);
        ogsm1.Title = "new name";
        
        // Act
        var result1 = service.GetSingleOgsm(ogsm1.ID);
        service.UpdateOgsm(ogsm1);
        var result2 = service.GetSingleOgsm(ogsm1.ID); 
        
        // Assert
        Assert.IsFalse(result1.Title == ogsm1.Title);
        Assert.IsTrue(result2.Title == ogsm1.Title);
    }
}