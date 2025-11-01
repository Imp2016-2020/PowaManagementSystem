using FluentAssertions;
using PowaManagementSystem.Helpers;

namespace PowaManagementSystem.Tests;

public class HashHelperTests
{
    [Fact]
    public void Md5_WithValidInput_ShouldReturnCorrectHash()
    {
        // Arrange
        var input = "password123";
        var expectedHash = "482c811da5d5b4bc6d497ffa98491e38"; // MD5 hash of "password123"

        // Act
        var result = HashHelper.Md5(input);

        // Assert
        result.Should().Be(expectedHash);
    }

    [Fact]
    public void Md5_WithEmptyString_ShouldReturnCorrectHash()
    {
        // Arrange
        var input = "";
        var expectedHash = "d41d8cd98f00b204e9800998ecf8427e"; // MD5 hash of empty string

        // Act
        var result = HashHelper.Md5(input);

        // Assert
        result.Should().Be(expectedHash);
    }

    [Fact]
    public void Md5_WithSpecialCharacters_ShouldReturnCorrectHash()
    {
        // Arrange
        var input = "!@#$%^&*()";

        // Act
        var result = HashHelper.Md5(input);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveLength(32); // MD5 hash is always 32 characters
        result.Should().MatchRegex("^[a-f0-9]{32}$"); // Should be lowercase hexadecimal
    }

    [Theory]
    [InlineData("test")]
    [InlineData("Password123")]
    [InlineData("admin@example.com")]
    public void Md5_WithDifferentInputs_ShouldAlwaysReturnLowercase32CharHex(string input)
    {
        // Act
        var result = HashHelper.Md5(input);

        // Assert
        result.Should().HaveLength(32);
        result.Should().MatchRegex("^[a-f0-9]{32}$");
    }

    [Fact]
    public void Md5_WithSameInput_ShouldReturnSameHash()
    {
        // Arrange
        var input = "consistent_input";

        // Act
        var result1 = HashHelper.Md5(input);
        var result2 = HashHelper.Md5(input);

        // Assert
        result1.Should().Be(result2);
    }
}
