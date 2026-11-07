namespace PortfolioSite.Core;

public class Project
{
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Description { get; set; } = "";
    public string Learned { get; set; } = "";
    public List<string> Tech { get; set; } = [];
    public string? Image { get; set; }
    public string RepoUrl { get; set; } = "";
    public string? DemoUrl { get; set; }
    public bool HasDemo => !string.IsNullOrWhiteSpace(DemoUrl);
    public string ImageOrPlaceholder => string.IsNullOrWhiteSpace(Image) ? "images/placeholder.png" : Image!;
}
