using FluentAssertions;
using PowaManagementSystem.Models;

namespace PowaManagementSystem.Tests.Models;

public class UserTests
{
    [Fact]
    public void User_DefaultConstructor_ShouldInitializeProperties()
    {
// Act
        var user = new User();

        // Assert
      user.Id.Should().Be(0);
        user.Username.Should().BeNull();
        user.Email.Should().BeNull();
        user.Password.Should().BeNull();
    }

    [Fact]
    public void User_PropertyAssignment_ShouldSetValuesCorrectly()
    {
        // Arrange
        var user = new User();
    const int expectedId = 1;
        const string expectedUsername = "testuser";
        const string expectedEmail = "test@example.com";
        const string expectedPassword = "hashedpassword";

    // Act
   user.Id = expectedId;
     user.Username = expectedUsername;
        user.Email = expectedEmail;
        user.Password = expectedPassword;

        // Assert
        user.Id.Should().Be(expectedId);
        user.Username.Should().Be(expectedUsername);
        user.Email.Should().Be(expectedEmail);
        user.Password.Should().Be(expectedPassword);
    }
}