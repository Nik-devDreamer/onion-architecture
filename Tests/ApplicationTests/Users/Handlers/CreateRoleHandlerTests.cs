using Application.Repositories;
using Application.Users.Commands;
using Application.Users.Handlers;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests.Users.Handlers;

[TestFixture]
public class CreateRoleHandlerTests
{
    [Test]
    public void Handle_ValidCommand_RoleCreatedTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        var tenantMock = new Mock<ITenant>(MockBehavior.Strict);
        var roleRepositoryMock = new Mock<IRoleRepository>(MockBehavior.Strict);

        var roleName = "TestRole";
        var command = new CreateRoleCommand(roleName);

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Roles).Returns(roleRepositoryMock.Object);
        roleRepositoryMock.Setup(repo => repo.Add(It.IsAny<Role>()));
        tenantMock.Setup(tenant => tenant.Commit());

        var createRoleHandler = new CreateRoleHandler(tenantFactoryMock.Object);

        // Act
        createRoleHandler.Handle(command);

        // Assert
        tenantFactoryMock.VerifyAll();
        tenantMock.VerifyAll();
        roleRepositoryMock.VerifyAll();
    }

    [Test]
    public void Handle_NullCommand_ThrowsArgumentNullExceptionTest()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateRoleHandler(null), "command");
    }
    
    [Test]
    public void Handle_NullCommandInHandle_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        var createRoleHandler = new CreateRoleHandler(tenantFactoryMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => createRoleHandler.Handle(null), "command");
    }
}