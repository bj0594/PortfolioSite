using PortfolioSite.Core;

namespace PortfolioSite.Web.Data;

public interface IProjectsClient
{
    Task<List<Project>> GetProjectsAsync();
}
