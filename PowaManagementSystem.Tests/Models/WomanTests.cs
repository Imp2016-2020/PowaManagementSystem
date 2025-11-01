using FluentAssertions;
using PowaManagementSystem.Models;

namespace PowaManagementSystem.Tests.Models;

public class WomanTests
{
    [Fact]
    public void Woman_DefaultConstructor_ShouldInitializeProperties()
    {
        // Act
    var woman = new Woman();

        // Assert
        woman.Id.Should().Be(0);
        woman.Firstname.Should().BeNull();
        woman.Lastname.Should().BeNull();
woman.Contact.Should().BeNull();
      woman.Address.Should().BeNull();
   woman.Age.Should().BeNull();
        woman.Employment.Should().BeNull();
    }

    [Fact]
    public void Woman_PropertyAssignment_ShouldSetValuesCorrectly()
    {
        // Arrange
        var woman = new Woman();
     const int expectedId = 1;
        const string expectedFirstname = "Jane";
        const string expectedLastname = "Doe";
        const string expectedContact = "123-456-7890";
        const string expectedAddress = "123 Main St";
        const string expectedAge = "30";
        const string expectedEmployment = "Teacher";

        // Act
        woman.Id = expectedId;
        woman.Firstname = expectedFirstname;
        woman.Lastname = expectedLastname;
        woman.Contact = expectedContact;
        woman.Address = expectedAddress;
     woman.Age = expectedAge;
        woman.Employment = expectedEmployment;

        // Assert
        woman.Id.Should().Be(expectedId);
 woman.Firstname.Should().Be(expectedFirstname);
        woman.Lastname.Should().Be(expectedLastname);
        woman.Contact.Should().Be(expectedContact);
    woman.Address.Should().Be(expectedAddress);
     woman.Age.Should().Be(expectedAge);
    woman.Employment.Should().Be(expectedEmployment);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
[InlineData(null)]
    public void Woman_WithInvalidFirstname_ShouldAllowButNotValidate(string invalidFirstname)
    {
      // Arrange
        var woman = new Woman();

        // Act
        woman.Firstname = invalidFirstname;

  // Assert
        woman.Firstname.Should().Be(invalidFirstname);
    }
}