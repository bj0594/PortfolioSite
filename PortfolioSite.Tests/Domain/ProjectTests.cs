using PortfolioSite.Core;

namespace PortfolioSite.Tests;

public class ProjectTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("https://example.com/", true)]
    public void HasDemo_DependsOnDemoUrl(string? optionalUrl, bool expected)
    {
        // Arrange
        var project = new Project { DemoUrl = optionalUrl };

        // Act and Assert
        Assert.Equal(expected, project.HasDemo);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ImageOrPlaceholder_NoImage_GivesThePlaceholder(string? image)
    {
        // Arrange
        var project = new Project { Image = image }
        ;

        // Act and Assert
        Assert.Equal("images/placeholder.png", project.ImageOrPlaceholder);
    }

    [Fact]
    public void ImageOrPlaceholder_WithImage_GivesTheImage()
    {
        // Arrange
        var project = new Project { Image = "images/first.png" };

        // Act and Assert
        Assert.Equal("images/first.png", project.ImageOrPlaceholder);
    }
}
