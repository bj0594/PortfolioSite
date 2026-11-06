using PortfolioSite.Core;

namespace PortfolioSite.Tests;

public class ProjectTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("https://example.com/", true)]
    public void HasDemo_DependsOnDemoUrl(string? optionalUrl, bool expected)
{
        // Arrange
        var project = new Project { DemoUrl = optionalUrl };

        // Act and Assert
        Assert.Equal(expected, project.HasDemo);
    }
}
