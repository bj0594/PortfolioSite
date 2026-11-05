using PortfolioSite.Core;

namespace PortfolioSite.Web.Data;

public class ProjectsClient(HttpClient http) : IProjectsClient
{
    public Task<List<Project>> GetProjectsAsync() => throw new NotImplementedException();
}
