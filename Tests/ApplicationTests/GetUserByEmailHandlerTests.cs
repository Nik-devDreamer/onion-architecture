using Application.Repositories;
using Application.Users.Handlers;
using Application.Users.Queries;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class GetUserByEmailHandlerTests
{
    [Test]
    public void Handle_ReturnsUser_WhenUserExistsTest()
    {
        // Arrange
        var userEmail = new Email("test@gmail.com");
        var user = new User(Guid.NewGuid(), "Test User", userEmail, Guid.NewGuid(), new Password("Test@123"));
        var query = new GetUserByEmailQuery(userEmail);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.TryGetByEmail(userEmail)).Returns(user);

        var tenantMock = new Mock<ITenant>();
        tenantMock.Setup(t => t.Users).Returns(userRepositoryMock.Object);

        var tenantFactoryMock = new Mock<ITenantFactory>();
        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);

        var handler = new GetUserByEmailHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.NotNull(result);
        Assert.That(result!.Id, Is.EqualTo(user.Id));
        Assert.That(result.Name, Is.EqualTo(user.Name));
        Assert.That(result.Email, Is.EqualTo(user.Email));
        Assert.That(result.RoleId, Is.EqualTo(user.RoleId));
        Assert.That(result.Password, Is.EqualTo(user.Password));
    }

    [Test]
    public void Handle_ReturnsNull_WhenUserDoesNotExistTest()
    {
        // Arrange
        var userEmail = new Email("test@gmail.com");
        var query = new GetUserByEmailQuery(userEmail);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.TryGetByEmail(userEmail)).Returns((User)null);

        var tenantMock = new Mock<ITenant>();
        tenantMock.Setup(t => t.Users).Returns(userRepositoryMock.Object);

        var tenantFactoryMock = new Mock<ITenantFactory>();
        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);

        var handler = new GetUserByEmailHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void Constructor_ThrowsArgumentNullException_WhenTenantFactoryIsNullTest()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetUserByEmailHandler(null));
    }
}