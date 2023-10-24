namespace nhs_ogsmTest;

[TestClass]
public class ExampleTest
{
    [TestMethod]
    public void ExampleTestMethod1()
    {
        //Arrange
        bool toTest = false;
        string toCheck = "checkme";
        
        //Act
        toTest = true;
        toCheck = toCheck.ToUpper();
        
        //Assert
        Assert.IsTrue(toTest);
        Assert.AreEqual("CHECKME", toCheck);
    }
}