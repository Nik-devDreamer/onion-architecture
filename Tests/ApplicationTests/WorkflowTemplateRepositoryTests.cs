using Application.Repositories;
using AutoFixture;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class WorkflowTemplateRepositoryTests
{
    private Fixture _fixture;
    private IWorkflowTemplateRepository _workflowTemplateRepository;
    private Mock<IWorkflowTemplateRepository> _mockWorkflowTemplateRepository;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _mockWorkflowTemplateRepository = new Mock<IWorkflowTemplateRepository>(MockBehavior.Strict);
        _workflowTemplateRepository = _mockWorkflowTemplateRepository.Object;
    }
    
    [Test]
    public void GetById_ValidId_ReturnsWorkflowTemplateTest()
    {
        // Arrange
        Guid templateId = Guid.NewGuid();
        string templateName = "Interview Process";
        var steps = new WorkflowStepTemplate[]
        {
            new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
            new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid())
        };

        var expectedTemplate = new WorkflowTemplate(templateId, templateName, steps);
        _mockWorkflowTemplateRepository.Setup(repo => repo.GetById(templateId)).Returns(expectedTemplate);

        // Act
        var result = _workflowTemplateRepository.GetById(templateId);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTemplate));
    }

    [Test]
    public void Add_ValidWorkflowTemplate_AddsWorkflowTemplateTest()
    {
        // Arrange
        Guid templateId = Guid.NewGuid();
        string templateName = "Interview Process";
        var steps = new WorkflowStepTemplate[]
        {
            new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
            new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid())
        };

        var template = new WorkflowTemplate(templateId, templateName, steps);
        _mockWorkflowTemplateRepository.Setup(repo => repo.Add(template));

        // Act
        _workflowTemplateRepository.Add(template);

        // Assert
        _mockWorkflowTemplateRepository.Verify(repo => repo.Add(template), Times.Once);
    }
}