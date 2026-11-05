using Bunit;
using Microsoft.Extensions.DependencyInjection;
using PortfolioSite.Core;
using PortfolioSite.Web.Data;
using PortfolioSite.Web.Pages;

namespace PortfolioSite.Web.Tests;

// Renders <Thing>DetailsPage with the fake client, so the tests need no file.
public class ProjectDetailsPageTests : BunitContext
{
    private readonly FakeProjectsClient _client = new ();

    public ProjectDetailsPageTests()
{
    Services.AddSingleton<IProjectsClient>(_client);
}

[Fact]
public void ProjectDetailsPage_KnownKey_ShowsTheDetails()
{
    // Arrange
    _client.Items.Add(new Project { Slug = "first", Title = "First", Description = "A library API", Summary = "TDD with xUnit" });

    // Act
    var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

    // Assert
    Assert.Contains("A library API", page.Markup);
    Assert.Contains("TDD with xUnit", page.Markup);
}
}
