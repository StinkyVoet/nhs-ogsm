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

    [TestMethod]
    public void GetOgsmsHierarchyTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "o1" };
        Ogsm ogsm2 = new Ogsm { Title = "o2" };
        Ogsm ogsm3 = new Ogsm { Title = "o3" };
        service.AddOgsm(ogsm1);
        service.AddOgsm(ogsm2);
        service.AddOgsm(ogsm3);
        
        Ogsm ogsm11 = new Ogsm { Title = "o11", ParentID = ogsm1.ID };
        Ogsm ogsm21 = new Ogsm { Title = "o21", ParentID = ogsm2.ID };
        service.AddOgsm(ogsm11);
        service.AddOgsm(ogsm21);
        
        // Act
        var result = service.GetOgsmsHierarchy(); 
        
        // Assert
        Assert.IsTrue(result.Contains(ogsm1));
        Assert.IsTrue(result.Contains(ogsm2));
        Assert.IsTrue(result.Contains(ogsm3));
        Assert.IsFalse(result.Contains(ogsm11));
        Assert.IsFalse(result.Contains(ogsm21));
    }
    
    
    [TestMethod]
    public void LoadChildrenTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "o1" };
        Ogsm ogsm2 = new Ogsm { Title = "o2" };
        service.AddOgsm(ogsm1);
        service.AddOgsm(ogsm2);
        
        Ogsm ogsm11 = new Ogsm { Title = "o11", ParentID = ogsm1.ID };
        Ogsm ogsm12 = new Ogsm { Title = "o12", ParentID = ogsm1.ID };
        Ogsm ogsm13 = new Ogsm { Title = "o13", ParentID = ogsm1.ID };
        service.AddOgsm(ogsm11);
        service.AddOgsm(ogsm12);
        service.AddOgsm(ogsm13);
        
        // Act
        var result = service.LoadChildren(ogsm1); 
        
        // Assert
        Assert.IsTrue(result.Contains(ogsm11));
        Assert.IsTrue(result.Contains(ogsm12));
        Assert.IsTrue(result.Contains(ogsm13));
        Assert.IsFalse(result.Contains(ogsm2));
    }
    
    
    [TestMethod]
    public void FindEldersTest()
    {
        // Arrange
        var service = new OgsmItemService(_contextFactory);
        Ogsm ogsm1 = new Ogsm { Title = "o1" };
        service.AddOgsm(ogsm1);
        
        Ogsm ogsm11 = new Ogsm { Title = "o11", ParentID = ogsm1.ID };
        Ogsm ogsm12 = new Ogsm { Title = "o12", ParentID = ogsm1.ID };
        service.AddOgsm(ogsm11);
        service.AddOgsm(ogsm12);
        
        Ogsm ogsm111 = new Ogsm { Title = "o111", ParentID = ogsm11.ID };
        service.AddOgsm(ogsm111);       
        Ogsm ogsm1111 = new Ogsm { Title = "o1111", ParentID = ogsm111.ID };
        service.AddOgsm(ogsm1111);
        
        // Act
        var result = service.FindElders(ogsm1111.ID, new List<int>()); 
        
        // Assert
        Assert.IsTrue(result.Contains(ogsm1.ID));
        Assert.IsTrue(result.Contains(ogsm11.ID));
        Assert.IsFalse(result.Contains(ogsm12.ID));
        Assert.IsTrue(result.Contains(ogsm111.ID));
    }
}