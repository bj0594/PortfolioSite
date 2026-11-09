using Bunit;
using Microsoft.Extensions.DependencyInjection;
using PortfolioSite.Core;
using PortfolioSite.Web.Data;
using PortfolioSite.Web.Pages;

namespace PortfolioSite.Web.Tests;

// Renders ProjectDetailsPage with the fake client, so the tests need no file.
public class ProjectDetailsPageTests : BunitContext
{
    private readonly FakeProjectsClient _client = new();

    public ProjectDetailsPageTests()
    {
        Services.AddSingleton<IProjectsClient>(_client);
    }

    [Fact]
    public void ProjectDetailsPage_KnownKey_ShowsTheDetails()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First", Description = "A library API", Learned = "TDD with xUnit" });

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

        // Assert
        Assert.Contains("A library API", page.Markup);
        Assert.Contains("TDD with xUnit", page.Markup);
    }

    [Fact]
    public void ProjectDetailsPage_UnknownKey_ShowsNotFound()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First" });

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "missing"));

        // Assert
        Assert.Contains("Project not found", page.Markup);
    }

    [Fact]
    public void ProjectDetailsPage_WithOptionalUrl_ShowsBothLinks()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First", RepoUrl = "https://github.com/me/first", DemoUrl = "https://me.github.io/first/" });

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

        // Assert
        Assert.Equal("https://github.com/me/first", page.Find("a.main-link").GetAttribute("href"));
        Assert.Equal("https://me.github.io/first/", page.Find("a.optional-link").GetAttribute("href"));
    }

    [Fact]
    public void ProjectDetailsPage_WithoutOptionalUrl_HasNoOptionalLink()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First", RepoUrl = "https://github.com/me/first" });

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

        // Assert
        Assert.Empty(page.FindAll("a.optional-link"));
    }

    [Fact]
    public void ProjectDetailsPage_FileCannotBeRead_ShowsCouldNotLoad()
    {
        // Arrange
        _client.Fail = true;

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

        // Assert
        Assert.Contains("Could not load projects", page.Markup);
    }

    [Fact]
    public void ProjectDetailsPage_NoImage_ShowsThePlaceholder()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First" });

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

        // Assert
        Assert.Equal("images/placeholder.png", page.Find("img").GetAttribute("src"));
    }

    [Fact]
    public void ProjectDetailsPage_WithOptionalUrl_ShowsTheLinkTexts()
    {
        // Arrange
        _client.Items.Add(new Project { Slug = "first", Title = "First", RepoUrl = "https://github.com/me/first", DemoUrl = "https://me.github.io/first/" });

        // Act
        var page = Render<ProjectDetailsPage>(parameters => parameters.Add(p => p.Slug, "first"));

        // Assert
        Assert.Equal("Repo on GitHub", page.Find("a.main-link").TextContent);
        Assert.Equal("Live demo", page.Find("a.optional-link").TextContent);
    }
}
