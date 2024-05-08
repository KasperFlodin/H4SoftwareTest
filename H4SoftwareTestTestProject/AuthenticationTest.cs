

using Bunit.TestDoubles;

namespace H4SoftwareTestTestProject;

public class AuthenticationTest
{
    [Fact]
    public void Athentication()
    {
        // Arrange
        var ctx = new TestContext();
        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorized("");

        // Act
        var cut = ctx.RenderComponent<Home>();

        // Assert
        cut.MarkupMatches("<h1>Hello, world!</h1>");


    }

    [Fact]
    public void AthenticationCode()
    {
        // Arrange
        var ctx = new TestContext();
        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorized("");

        // Act
        var cut = ctx.RenderComponent<Home>();
        var homeObj = cut.Instance;

        // Assert
        Assert.Equal(homeObj._isAuthenticated, true);


    }
}