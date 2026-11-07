using Bunit;
using PortfolioSite.Web.Layout;

namespace PortfolioSite.Web.Tests;

// The menu links the front page.
public class MainLayoutTests : BunitContext
{
    [Fact]
    public void MainLayout_Always_LinksTheFrontPage()
    {
        // Act
        var layout = Render<MainLayout>(p => p.Add(l => l.Body, "<p>page</p>"));

        // Assert
        var links = layout.FindAll("nav a").Select(a => a.GetAttribute("href")).ToList();
        Assert.Contains("", links);
    }
}
