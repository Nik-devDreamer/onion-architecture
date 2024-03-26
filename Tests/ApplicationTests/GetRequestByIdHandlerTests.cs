using Application.Repositories;
using Application.Requests.Handlers;
using Application.Requests.Queries;
using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using Domain.Entities.Users;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class GetRequestByIdHandlerTests
{
    private Fixture _fixture;

    public GetRequestByIdHandlerTests()
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
    public void Handle_ExistingRequestId_ReturnsRequestTest()
    {
        // Arrange
        var requestId = Guid.NewGuid();
        var (request, _) = CreateRequest();
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var requestRepositoryMock = new Mock<IRequestRepository>();

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Requests).Returns(requestRepositoryMock.Object);
        requestRepositoryMock.Setup(repo => repo.GetById(requestId)).Returns(request);

        var handler = new GetRequestByIdHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(new GetRequestByIdQuery(requestId));

        // Assert
        Assert.That(result, Is.EqualTo(request));
    }

    [Test]
    public void Handle_NonExistingRequestId_ReturnsNullTest()
    {
        // Arrange
        var requestId = Guid.NewGuid();
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var requestRepositoryMock = new Mock<IRequestRepository>();

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Requests).Returns(requestRepositoryMock.Object);
        requestRepositoryMock.Setup(repo => repo.GetById(requestId)).Returns((Request)null);

        var handler = new GetRequestByIdHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(new GetRequestByIdQuery(requestId));

        // Assert
        Assert.IsNull(result);
    }
}