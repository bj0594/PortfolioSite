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
        Assert.Equal(["first", "second", "third"], result.Select(i => i.Slug));
    }

    [Fact]
    public void GetProject_KnownKey_ReturnsThatOne()
{
        // Arrange
        var service = new ProjectService([new Project { Slug = "first" }, new Project { Slug = "second" }]);

        // Act
        var result = service.GetProject("second");

        // Assert
        Assert.Equal("second", result.Slug);
    }

    [Fact]
    public void GetProject_UnknownKey_ThrowsKeyNotFoundException()
    {
        // Arrange
        var service = new ProjectService([new Project { Slug = "first" }]);

        // Act and Assert
        Assert.Throws<KeyNotFoundException>(() => service.GetProject("missing"));
    }
}
