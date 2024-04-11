using Application.Repositories;
using Application.Requests.Commands;
using Application.Requests.Handlers;
using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using Domain.Entities.Users;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests.Requests.Handlers;

[TestFixture]
public class RejectRequestHandlerTests
{
    private Fixture _fixture;

    public RejectRequestHandlerTests()
    {
        _fixture = new Fixture();
    }

    private (Request request, User user) CreateRequest()
    {
        var name = _fixture.Create<string>();
        var email = new Email(_fixture.Create<string>() + "@gmail.com");
        var role = new Role("TestRole");
        var password = new Password("Test@123");
        var document = new Document(email, name, "1234567890", DateTime.Now);
        var user = User.Create(name, email, role, password);
        List<WorkflowStepTemplate> steps = CreateDefaultSteps(user.Id, role.Id);
        WorkflowTemplate workflowTemplate = new WorkflowTemplate(Guid.NewGuid(), "HR", steps.ToArray());
        var request = workflowTemplate.CreateRequest(user, document);
        return (request, user);
    }

    private static List<WorkflowStepTemplate> CreateDefaultSteps(Guid userId, Guid roleGuid)
    {
        return new List<WorkflowStepTemplate>
        {
            new WorkflowStepTemplate("Online Interview", 1, userId, roleGuid),
            new WorkflowStepTemplate("Interview with HR", 2, userId, roleGuid),
            new WorkflowStepTemplate("Technical Task", 3, userId, roleGuid),
            new WorkflowStepTemplate("Meeting with CEO", 4, userId, roleGuid),
        };
    }

    [Test]
    public void Handle_ValidCommand_RequestRejectedTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        var tenantMock = new Mock<ITenant>(MockBehavior.Strict);
        var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
        var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

        var (request, user) = CreateRequest();

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Requests).Returns(requestRepositoryMock.Object);
        tenantMock.Setup(tenant => tenant.Users).Returns(userRepositoryMock.Object);
        requestRepositoryMock.Setup(repo => repo.GetById(request.Id)).Returns(request);
        userRepositoryMock.Setup(repo => repo.GetById(user.Id)).Returns(user);
        tenantMock.Setup(tenant => tenant.Commit()).Verifiable();

        var handler = new RejectRequestHandler(tenantFactoryMock.Object);
        var command = new RejectRequestCommand(user.Id, request.Id);

        // Act
        handler.Handle(command);

        // Assert
        requestRepositoryMock.VerifyAll();
        userRepositoryMock.VerifyAll();
        requestRepositoryMock.VerifyAll();
        tenantMock.VerifyAll();
        Assert.IsTrue(request.IsRejected());
    }
    
    [Test]
    public void Handle_NullCommand_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        var handler = new RejectRequestHandler(tenantFactoryMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => handler.Handle(null));
    }
}