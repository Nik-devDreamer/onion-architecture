using Application.Repositories;
using Application.Users.Handlers;
using Application.Users.Queries;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class GetAllUsersHandlerTests
{
    [Test]
    public void Handle_ReturnsListOfUsers_WhenUsersExistTest()
    {
        // Arrange
        var users = new List<User>
        {
            new User(Guid.NewGuid(), "Name1", new Email("email1@gmail.com"), Guid.NewGuid(), new Password("Test@123")),
            new User(Guid.NewGuid(), "Name2", new Email("email2@gmail.com"), Guid.NewGuid(), new Password("Test@1234"))
        };

        var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
        userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users);

        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        tenantFactoryMock.Setup(factory => factory.GetTenant().Users).Returns(userRepositoryMock.Object);

        var getAllUsersHandler = new GetAllUsersHandler(tenantFactoryMock.Object);

        // Act
        var result = getAllUsersHandler.Handle(new GetAllUsersQuery());

        // Assert
        Assert.That(result.Count, Is.EqualTo(users.Count));
        Assert.IsTrue(result.Contains(users[0]));
        Assert.IsTrue(result.Contains(users[1]));
    }

    [Test]
    public void Handle_ReturnsEmptyList_WhenNoUsersExistTest()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
        userRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<User>());

        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        tenantFactoryMock.Setup(factory => factory.GetTenant().Users).Returns(userRepositoryMock.Object);

        var getAllUsersHandler = new GetAllUsersHandler(tenantFactoryMock.Object);

        // Act
        var result = getAllUsersHandler.Handle(new GetAllUsersQuery());

        // Assert
        Assert.IsEmpty(result);
    }
}