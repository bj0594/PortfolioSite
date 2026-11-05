using PortfolioSite.Web.Data;

namespace PortfolioSite.Web.Tests;

// Tests the real client against a file the test gives it.
public class ProjectClientTests
{
    private readonly StubHttpMessageHandler _handler = new();
private readonly ProjectsClient _client;

public ProjectClientTests()
{
    _client = new ProjectsClient(new HttpClient(_handler) { BaseAddress = new Uri("http://site.test/") });
}

[Fact]
public async Task GetProjectsAsync_FileHasTwo_ReturnsBothInOrder()
    {
    // Arrange
    _handler.Respond("/data/projects.json", """[{ "slug": "first" }, { "slug": "second" }]""");

    // Act
    var result = await _client.GetProjectsAsync();

    // Assert
    Assert.Equal(new[] { "first", "second" }, result.Select(p => p.Slug));
}
}
