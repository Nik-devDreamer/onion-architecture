using Application.Users.Commands;
using Domain.BaseObjectsNamespace;
using NUnit.Framework;

namespace ApplicationTests.Users.Commands;

[TestFixture]
public class CreateUserCommandTests
{
    [Test]
    public void Constructor_NullName_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        string name = null;
        Email email = new Email("test@gmail.com");
        var roleId = Guid.NewGuid();
        Password password = new Password("Test@123");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateUserCommand(name, email, roleId, password));
    }

    [Test]
    public void Constructor_EmptyEmail_ThrowsArgumentExceptionTest()
    {
        // Arrange
        string name = "TestUser";
        Email email = null;
        var roleId = Guid.NewGuid();
        Password password = new Password("Test@123");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateUserCommand(name, email, roleId, password));
    }

    [Test]
    public void Constructor_InvalidRoleId_ThrowsArgumentExceptionTest()
    {
        // Arrange
        string name = "TestUser";
        Email email = new Email("test@gmail.com");
        var roleId = Guid.Empty;
        Password password = new Password("Test@123");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CreateUserCommand(name, email, roleId, password));
    }

    [Test]
    public void Constructor_NullPassword_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        string name = "TestUser";
        Email email = new Email("test@gmail.com");
        var roleId = Guid.NewGuid();
        Password password = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateUserCommand(name, email, roleId, password));
    }

    [Test]
    public void Constructor_ValidArguments_CreatesInstanceTest()
    {
        // Arrange
        string name = "TestUser";
        Email email = new Email("test@gmail.com");
        var roleId = Guid.NewGuid();
        Password password = new Password("Test@123");

        // Act
        var command = new CreateUserCommand(name, email, roleId, password);

        // Assert
        Assert.That(command.Name, Is.EqualTo(name));
        Assert.That(command.Email, Is.EqualTo(email));
        Assert.That(command.RoleId, Is.EqualTo(roleId));
        Assert.That(command.Password, Is.EqualTo(password));
    }
}