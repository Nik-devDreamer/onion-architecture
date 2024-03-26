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
public class GetAllRequestsHandlerTests
{
    private Fixture _fixture;

    [SetUp]
    public void Setup()
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
    public void Handle_ReturnsAllRequestsTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var requestRepositoryMock = new Mock<IRequestRepository>();
        var expectedRequests = new List<Request>();
        for (int i = 0; i < 5; i++)
        {
            var (request, _) = CreateRequest();
            expectedRequests.Add(request);
        }
        
        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Requests).Returns(requestRepositoryMock.Object);
        requestRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedRequests);

        var handler = new GetAllRequestsHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(new GetAllRequestsQuery());

        // Assert
        CollectionAssert.AreEqual(expectedRequests, result);
    }

    [Test]
    public void Handle_NoRequests_ReturnsEmptyCollectionTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var requestRepositoryMock = new Mock<IRequestRepository>();
        var expectedRequests = new List<Request>();
        
        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.Requests).Returns(requestRepositoryMock.Object);
        requestRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedRequests);

        var handler = new GetAllRequestsHandler(tenantFactoryMock.Object);

        // Act
        var result = handler.Handle(new GetAllRequestsQuery());

        // Assert
        CollectionAssert.AreEqual(expectedRequests, result);
    }
}