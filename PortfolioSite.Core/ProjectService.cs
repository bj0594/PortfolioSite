namespace PortfolioSite.Core;

public class ProjectService(List<Project> projects)
{
    private readonly List<Project> _projects = projects;

    public List<Project> GetProjects() => _projects;
    public Project GetProject(string slug) =>
        _projects.FirstOrDefault(p => p.Slug == slug)
        ?? throw new KeyNotFoundException($"No project with slug '{slug}'");
}
