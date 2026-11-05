namespace PortfolioSite.Core;

public class ProjectService
{
    private readonly List<Project> _projects;
    public ProjectService(List<Project> projects)
    {
        _projects = projects;
    }

    public List<Project> GetProjects() => _projects;
}
