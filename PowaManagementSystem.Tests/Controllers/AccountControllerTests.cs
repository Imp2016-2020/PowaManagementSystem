using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using PowaManagementSystem.Controllers;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using PowaManagementSystem.Tests.Infrastructure;

namespace PowaManagementSystem.Tests.Controllers;

public class AccountControllerTests : IDisposable
{
    private readonly PowaDbContext _context;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        var options = new DbContextOptionsBuilder<PowaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new PowaDbContext(options);
        _controller = new AccountController(_context);

        var httpContext = new DefaultHttpContext();
        httpContext.Session = new TestSession();
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
    }

    [Fact]
    public void Register_Get_ShouldReturnViewWithNewUserModel()
    {
        // Act
        var result = _controller.Register();

        // Assert
        result.Should().BeOfType<ViewResult>();
        var viewResult = result as ViewResult;
        viewResult!.Model.Should().BeOfType<User>();
    }

    [Fact]
    public void Register_Post_WithValidData_ShouldCreateUserAndRedirectToLogin()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            Email = "test@example.com"
        };
        const string password1 = "password123";
        const string password2 = "password123";

        // Act
        var result = _controller.Register(user, password1, password2);

        // Assert
        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Login");

        var savedUser = _context.Users.FirstOrDefault(u => u.Username == "testuser");
        savedUser.Should().NotBeNull();
        savedUser!.Email.Should().Be("test@example.com");
    }

    [Fact]
    public void Register_Post_WithMismatchedPasswords_ShouldReturnViewWithErrors()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            Email = "test@example.com"
        };
        const string password1 = "password123";
        const string password2 = "differentpassword";

        // Act
        var result = _controller.Register(user, password1, password2);

        // Assert
        result.Should().BeOfType<ViewResult>();
        var errorsList = (List<string>)_controller.ViewBag.Errors;
        errorsList.Should().NotBeNull();
        errorsList!.Should().Contain("Passwords do not match");
    }

    [Fact]
    public void Register_Post_WithExistingUsername_ShouldReturnViewWithErrors()
    {
        // Arrange
        _context.Users.Add(new User
        {
            Username = "existinguser",
            Email = "existing@example.com",
            Password = "hashedpassword"
        });
        _context.SaveChanges();

        var user = new User
        {
            Username = "existinguser",
            Email = "newemail@example.com"
        };
        const string password1 = "password123";
        const string password2 = "password123";

        // Act
        var result = _controller.Register(user, password1, password2);

        // Assert
        result.Should().BeOfType<ViewResult>();
        var errorsList = (List<string>)_controller.ViewBag.Errors;
        errorsList.Should().NotBeNull();
        errorsList!.Should().Contain("Username already exists");
    }

    [Theory]
    [InlineData("", "email@test.com", "pass", "pass")]
    [InlineData("username", "", "pass", "pass")]
    [InlineData("username", "email@test.com", "", "pass")]
    [InlineData("username", "email@test.com", "pass", "")]
    public void Register_Post_WithMissingRequiredFields_ShouldReturnViewWithErrors(
   string username, string email, string password1, string password2)
    {
        // Arrange
        var user = new User
        {
            Username = username,
            Email = email
        };

        // Act
        var result = _controller.Register(user, password1, password2);

        // Assert
        result.Should().BeOfType<ViewResult>();
        var errorsList = (List<string>)_controller.ViewBag.Errors;
        errorsList.Should().NotBeNull();
        errorsList!.Should().NotBeEmpty();
    }

    [Fact]
    public void Login_Get_ShouldReturnView()
    {
        // Act
        var result = _controller.Login();

        // Assert
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Login_Post_WithValidCredentials_ShouldRedirectToHome()
    {
        // Arrange
        const string password = "password123";
        const string hashedPassword = "482c811da5d5b4bc6d497ffa98491e38"; // MD5 of "password123"

        _context.Users.Add(new User
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword
        });
        _context.SaveChanges();

        // Act
        var result = _controller.Login("testuser", password);

        // Assert
        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Index");
        redirectResult.ControllerName.Should().Be("Home");
    }

    [Fact]
    public void Login_Post_WithInvalidCredentials_ShouldReturnViewWithErrors()
    {
        // Act
        var result = _controller.Login("nonexistentuser", "wrongpassword");

        // Assert
        result.Should().BeOfType<ViewResult>();
        var errorsList = (List<string>)_controller.ViewBag.Errors;
        errorsList.Should().NotBeNull();
        errorsList!.Should().Contain("Wrong username/password");
    }

    [Theory]
    [InlineData("", "password")]
    [InlineData("username", "")]
    [InlineData("", "")]
    public void Login_Post_WithMissingCredentials_ShouldReturnViewWithErrors(string username, string password)
    {
        // Act
        var result = _controller.Login(username, password);

        // Assert
        result.Should().BeOfType<ViewResult>();
        var errorsList = (List<string>)_controller.ViewBag.Errors;
        errorsList.Should().NotBeNull();
        errorsList!.Should().NotBeEmpty();
    }

    [Fact]
    public void Logout_ShouldClearSessionAndRedirectToLogin()
    {
        // Act
        var result = _controller.Logout();

        // Assert
        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Login");
    }

    public void Dispose()
    {
        _context.Dispose();
        _controller.Dispose();
    }
}