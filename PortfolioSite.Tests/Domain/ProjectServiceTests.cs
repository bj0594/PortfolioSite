using System.Diagnostics;
using PortfolioSite.Core;

namespace PortfolioSite.Tests;

public class ProjectServiceTests
{
    [Fact]
    public void GetProjects_Threegiven_ReturnsAllInTheSameOrder()
    {
        // Arrange
        var projects = new List<Project>
        {
            new() { Slug = "first" },
            new() { Slug = "second" },
            new() { Slug = "third" },
        };
        var service = new ProjectService(projects);

        // Act
        var result = service.GetProjects();

        // Assert
        Assert.Equal(new[] { "first", "second", "third" }, result.Select(i => i.Slug));
    }
}
