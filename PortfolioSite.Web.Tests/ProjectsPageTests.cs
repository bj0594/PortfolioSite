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
}
