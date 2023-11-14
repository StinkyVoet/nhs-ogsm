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
    
    
    [TestMethod]
    public void AddOgsmTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        
        // Act
        service.AddOgsm(new Ogsm { Title = "Testing AddOgsm" });
        var result = service.GetAllOgsm(); 
        
        // Assert
        Assert.IsNotNull(result);
        // mockContext.Verify(context => context.SaveChanges(), Times.Once);
    }
    
    [TestMethod]
    public void DeleteOgsmTest()
    {
        // Arrange

        // Act
        
        // Assert
    }
}