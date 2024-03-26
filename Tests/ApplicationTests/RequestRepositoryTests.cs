using Application.Repositories;
using Application.Requests.Commands;
using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using Domain.Entities.Users;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class RequestRepositoryTests
{
    private Fixture _fixture;
    private IRequestRepository _requestRepository;
    private Mock<IRequestRepository> _mockRequestRepository;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _mockRequestRepository = new Mock<IRequestRepository>();
        _requestRepository = _mockRequestRepository.Object;
    }

    private Document CreateDocument()
    {
        var email = new Email("test@gmail.com");
        var name = "testName";
        var phoneNumber = "+1234567890";
        var dateOfBirth = new DateTime(2000, 2, 2);
        return new Document(email, name, phoneNumber, dateOfBirth);
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
    public void GetById_ValidId_ReturnsRequestTest()
    {
        // Arrange
        var (request, user) = CreateRequest();
        _mockRequestRepository.Setup(repo => repo.GetById(request.Id)).Returns(request);

        // Act
        var result = _requestRepository.GetById(request.Id);

        // Assert
        Assert.That(result, Is.EqualTo(request));
    }

    [Test]
    public void Add_ValidRequest_AddsRequestTest()
    {
        // Arrange
        var (request, user) = CreateRequest();

        // Act
        _requestRepository.Add(request);

        // Assert
        _mockRequestRepository.Verify(repo => repo.Add(request), Times.Once);
    }

    [Test]
    public void Create_ValidCommand_CreatesRequestTest()
    {
        // Arrange
        var command = new CreateRequestCommand(Guid.NewGuid(), CreateDocument(), Guid.NewGuid());

        // Act
        _requestRepository.Create(command);

        // Assert
        _mockRequestRepository.Verify(repo => repo.Create(command), Times.Once);
    }

    [Test]
    public void GetAll_ReturnsAllRequestsTest()
    {
        // Arrange
        var expectedRequests = new List<Request>();
        for (int i = 0; i < 5; i++)
        {
            var (request, _) = CreateRequest();
            expectedRequests.Add(request);
        }
    
        _mockRequestRepository.Setup(repo => repo.GetAll()).Returns(expectedRequests);

        // Act
        var result = _requestRepository.GetAll();

        // Assert
        CollectionAssert.AreEqual(expectedRequests, result);
    }
}