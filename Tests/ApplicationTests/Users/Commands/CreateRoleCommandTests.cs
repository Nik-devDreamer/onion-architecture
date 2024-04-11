using Application.Users.Commands;
using NUnit.Framework;

namespace ApplicationTests.Users.Commands;

[TestFixture]
public class CreateRoleCommandTests
{
    [Test]
    public void Constructor_ValidParameters_PropertiesSetCorrectlyTest()
    {
        // Arrange
        string roleName = "RoleName";

        // Act
        var command = new CreateRoleCommand(roleName);

        // Assert
        Assert.That(command.Name, Is.EqualTo(roleName));
    }

    [Test]
    public void Constructor_NullRoleName_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        string roleName = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateRoleCommand(roleName));
    }
}