using Application.Repositories;
using Application.Users.Commands;
using Application.Users.Handlers;
using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class CreateUserHandlerTests
{
    private Fixture _fixture;

    public CreateUserHandlerTests()
    {
        _fixture = new Fixture();
    }
    
    [Test]
    public void Handle_ValidCommand_UserCreatedTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var roleRepositoryMock = new Mock<IRoleRepository>();

        var name = _fixture.Create<string>();
        var email = new Email(_fixture.Create<string>() + "@gmail.com");
        var password = new Password("Test@123");
        Role role = new Role(name);
        User user = new User(new Guid(), name, email, role.Id, password);

        var createUserHandler = new CreateUserHandler(tenantFactoryMock.Object);
        var command = new CreateUserCommand(name, email, role.Id, password);

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Users).Returns(userRepositoryMock.Object);
        tenantMock.Setup(tenant => tenant.Roles).Returns(roleRepositoryMock.Object);
        userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).Callback<User>(u => user = u);
        roleRepositoryMock.Setup(repo => repo.GetById(role.Id)).Returns(role);

        // Act
        createUserHandler.Handle(command);

        // Assert
        userRepositoryMock.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        Assert.NotNull(user);
        Assert.That(user.Name, Is.EqualTo(name));
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.RoleId, Is.EqualTo(role.Id));
        Assert.That(user.Password, Is.EqualTo(password));
    }

    [Test]
    public void Handle_NullCommand_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var createUserHandler = new CreateUserHandler(tenantFactoryMock.Object);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => createUserHandler.Handle(null));
    }
    
    [Test]
    public void Handle_UserWithEmailAlreadyExists_ThrowsInvalidOperationException()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var roleRepositoryMock = new Mock<IRoleRepository>();

        var name = _fixture.Create<string>();
        var email = new Email(_fixture.Create<string>() + "@gmail.com");
        var password = new Password("Test@123");
        Role role = new Role(name);

        userRepositoryMock.Setup(repo => repo.TryGetByEmail(email)).Returns(new User(Guid.NewGuid(), "ExistingUser", email, role.Id, password));

        var createUserHandler = new CreateUserHandler(tenantFactoryMock.Object);
        var command = new CreateUserCommand(name, email, role.Id, password);

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Users).Returns(userRepositoryMock.Object);
        tenantMock.Setup(tenant => tenant.Roles).Returns(roleRepositoryMock.Object);
        roleRepositoryMock.Setup(repo => repo.GetById(role.Id)).Returns(role);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => createUserHandler.Handle(command));
    }
}