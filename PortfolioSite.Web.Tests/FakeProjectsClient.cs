using PortfolioSite.Core;
using PortfolioSite.Web.Data;

namespace PortfolioSite.Web.Tests;

// Keeps the projects in memory, so the page tests need no file.
public class FakeProjectsClient : IProjectsClient
{
    public List<Project> Items { get; } = [];
    public bool Fail { get; set; }

    public Task<List<Project>> GetProjectsAsync() =>
        Fail ? throw new HttpRequestException("Could not load") : Task.FromResult(Items.ToList());
}
