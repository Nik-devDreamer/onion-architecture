using Application.Repositories;
using Application.Users.Commands;
using Application.Users.Handlers;
using AutoFixture;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class CreateRoleHandlerTests
{
    [Test]
    public void Handle_ValidCommand_RoleCreatedTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var roleRepositoryMock = new Mock<IRoleRepository>();

        var roleName = "TestRole";
        var command = new CreateRoleCommand(roleName);

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Roles).Returns(roleRepositoryMock.Object);

        var createRoleHandler = new CreateRoleHandler(tenantFactoryMock.Object);

        // Act
        createRoleHandler.Handle(command);

        // Assert
        roleRepositoryMock.Verify(repo => repo.Add(It.IsAny<Role>()), Times.Once);
    }

    [Test]
    public void Handle_NullCommand_ThrowsArgumentNullExceptionTest()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateRoleHandler(null), "command");
    }
}