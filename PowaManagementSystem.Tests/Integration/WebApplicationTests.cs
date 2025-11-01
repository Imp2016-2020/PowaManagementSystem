using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace PowaManagementSystem.Tests.Integration;

public class WebApplicationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WebApplicationTests(WebApplicationFactory<Program> factory)
    {
      _factory = factory;
    }

    [Fact]
    public async Task HomePage_ShouldReturnSuccessOrRedirect()
    {
      // Arrange
        var client = _factory.CreateClient();

        // Act
     var response = await client.GetAsync("/");

        // Assert
     // The home page should return 200 OK (success) or redirect to login
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect, HttpStatusCode.Found);
  }

    [Theory]
    [InlineData("/Account/Login")]
 [InlineData("/Account/Register")]
    [InlineData("/Home/Privacy")]
  public async Task PublicPages_ShouldReturnSuccessStatusCode(string url)
  {
   // Arrange
      var client = _factory.CreateClient();

     // Act
      var response = await client.GetAsync(url);

    // Assert
      response.EnsureSuccessStatusCode();
   response.Content.Headers.ContentType?.ToString().Should().StartWith("text/html");
    }

    [Fact]
    public void Application_ShouldStartupCorrectly()
    {
      // Arrange & Act
        var client = _factory.CreateClient();

   // Assert
        client.Should().NotBeNull();
     _factory.Should().NotBeNull();
    }
}