using Application.Repositories;
using Application.Users.Handlers;
using Application.Users.Queries;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests.Users.Handlers;

[TestFixture]
public class GetRoleByIdHandlerTests
{
    [Test]
    public void Handle_ReturnsCorrectRole_WhenRoleExistsTest()
    {
        // Arrange
        var role = new Role("Role");
        
        var query = new GetRoleByIdQuery(role.Id);
        
        var roleRepositoryMock = new Mock<IRoleRepository>(MockBehavior.Strict);
        roleRepositoryMock.Setup(repo => repo.GetById(role.Id)).Returns(role);
        
        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        tenantFactoryMock.Setup(factory => factory.GetTenant().Roles).Returns(roleRepositoryMock.Object);
        
        var handler = new GetRoleByIdHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(role.Id));
        Assert.That(result.Name, Is.EqualTo("Role"));
    }
}