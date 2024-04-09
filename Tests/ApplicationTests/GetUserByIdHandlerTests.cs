using Application.Repositories;
using Application.Users.Handlers;
using Application.Users.Queries;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class GetUserByIdHandlerTests
{
    [Test]
    public void Handle_ReturnsUser_WhenUserExistsTest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUser", new Email("test@gmail.com"), Guid.NewGuid(), new Password("Test@123"));
        var query = new GetUserByIdQuery(userId);

        var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
        userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(user);

        var tenantMock = new Mock<ITenant>(MockBehavior.Strict);
        tenantMock.Setup(t => t.Users).Returns(userRepositoryMock.Object);

        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);

        var handler = new GetUserByIdHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Id, Is.EqualTo(user.Id));
    }

    [Test]
    public void Constructor_ThrowsArgumentNullException_WhenTenantFactoryIsNullTest()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetUserByIdHandler(null));
    }
}