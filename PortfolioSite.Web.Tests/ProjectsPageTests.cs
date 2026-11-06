using Bunit;
using Microsoft.Extensions.DependencyInjection;
using PortfolioSite.Core;
using PortfolioSite.Web.Data;
using PortfolioSite.Web.Pages;

namespace PortfolioSite.Web.Tests;

// Renders ProjectsPage with the fake client, so the tests need no file.
public class ProjectsPageTests : BunitContext
{
    private readonly FakeProjectsClient _client = new ();

    public ProjectsPageTests()
    {
        Services.AddSingleton<IProjectsClient>(_client);
    }

    [Fact]
    public void ProjectsPage_TwoInTheFile_ShowsOneCardEach()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First" });
        _client.Items.Add(new Project { Slug = "second", Title = "Second" });

        // Act
        var page = Render <ProjectsPage> ();

        // Assert
        Assert.Equal(2, page.FindAll(".project-card").Count);
    }

    [Fact]
    public void ProjectsPage_TwoInTheFile_EachCardLinksToItsPage()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First" });
        _client.Items.Add(new Project { Slug = "second", Title = "Second" });

        // Act
        var page = Render< ProjectsPage>();

        // Assert
        Assert.Equal("projects/second", page.FindAll(".project-card a")[1].GetAttribute("href"));
    }

    [Fact]
    public void ProjectsPage_EmptyFile_ShowsNoneYet()
    {
        // Act
        var page = Render<ProjectsPage>();

        // Assert
        Assert.Contains("No projects yet", page.Markup);
    }

    [Fact]
    public void ProjectsPage_FileCannotBeRead_ShowsCouldNotLoad()
    {
        // Arrange
        _client.Fail = true;

        // Act
        var page = Render<ProjectsPage>();

        // Assert
        Assert.Contains("Could not load projects", page.Markup);
    }
}
