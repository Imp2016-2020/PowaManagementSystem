using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PowaManagementSystem.Controllers;
using PowaManagementSystem.Models;
using PowaManagementSystem.Tests.Infrastructure;

namespace PowaManagementSystem.Tests.Controllers;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _mockLogger;
 private readonly HomeController _controller;

    public HomeControllerTests()
    {
  _mockLogger = new Mock<ILogger<HomeController>>();
        _controller = new HomeController(_mockLogger.Object);
    }

   [Fact]
    public void Index_WithNoSession_ShouldRedirectToLogin()
    {
    // Arrange
  var mockHttpContext = new Mock<HttpContext>();
      var testSession = new TestSession(); // Use our test session implementation
  
     mockHttpContext.Setup(c => c.Session).Returns(testSession);
   _controller.ControllerContext = new ControllerContext
{
       HttpContext = mockHttpContext.Object
      };

        // Act
        var result = _controller.Index();

     // Assert
   result.Should().BeOfType<RedirectToActionResult>();
      var redirectResult = result as RedirectToActionResult;
  redirectResult!.ActionName.Should().Be("Login");
    redirectResult.ControllerName.Should().Be("Account");
    }

  [Fact]
    public void Index_WithValidSession_ShouldReturnViewWithUsername()
    {
      // Arrange
const string username = "testuser";
 var mockHttpContext = new Mock<HttpContext>();
     var testSession = new TestSession();
  
      // Set the username in session
     testSession.SetString("Username", username);
    
    mockHttpContext.Setup(c => c.Session).Returns(testSession);
      _controller.ControllerContext = new ControllerContext
  {
   HttpContext = mockHttpContext.Object
    };

      // Act
 var result = _controller.Index();

  // Assert
     result.Should().BeOfType<ViewResult>();
     string? actualUsername = _controller.ViewBag.Username as string;
actualUsername.Should().Be(username);
 }

    [Fact]
    public void Privacy_ShouldReturnView()
{
        // Act
 var result = _controller.Privacy();

        // Assert
    result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Error_ShouldReturnViewWithErrorViewModel()
    {
     // Arrange
     var mockHttpContext = new Mock<HttpContext>();
      mockHttpContext.Setup(c => c.TraceIdentifier).Returns("test-trace-id");
    _controller.ControllerContext = new ControllerContext
        {
     HttpContext = mockHttpContext.Object
    };

        // Act
   var result = _controller.Error();

 // Assert
  result.Should().BeOfType<ViewResult>();
        var viewResult = result as ViewResult;
   viewResult!.Model.Should().BeOfType<ErrorViewModel>();
        var errorModel = viewResult.Model as ErrorViewModel;
    errorModel!.RequestId.Should().Be("test-trace-id");
    }
}