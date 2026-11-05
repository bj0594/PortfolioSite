using PortfolioSite.Core;
using System.Net.Http.Json;

namespace PortfolioSite.Web.Data;

public class ProjectsClient(HttpClient http) : IProjectsClient
{
    public async Task<List<Project>> GetProjectsAsync()
    => await http.GetFromJsonAsync<List<Project>>("data/projects.json") ?? [];
}
