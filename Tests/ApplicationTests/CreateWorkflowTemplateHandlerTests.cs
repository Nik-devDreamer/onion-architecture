using Application.Repositories;
using Application.WorkflowTemplates.Commands;
using Application.WorkflowTemplates.Handlers;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class CreateWorkflowTemplateHandlerTests
{
    [Test]
    public void Handle_ReturnsWorkflowTemplateId_WhenCommandIsValidTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>(MockBehavior.Strict);
        var tenantMock = new Mock<ITenant>(MockBehavior.Strict);
        var workflowRepositoryMock = new Mock<IWorkflowTemplateRepository>(MockBehavior.Strict);

        var name = "TestWorkflow";
        WorkflowStepTemplate[] steps = new WorkflowStepTemplate[2]
        {
            new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
            new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid())
        };

        var command = new CreateWorkflowTemplateCommand(name, steps);

        var handler = new CreateWorkflowTemplateHandler(tenantFactoryMock.Object);

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.WorkflowsTemplate).Returns(workflowRepositoryMock.Object);
        workflowRepositoryMock.Setup(repo => repo.Add(It.IsAny<WorkflowTemplate>()));
        tenantMock.Setup(tenant => tenant.Commit());

        // Act
        var result = handler.Handle(command);

        // Assert
        Assert.That(result, Is.Not.EqualTo(Guid.Empty));
        workflowRepositoryMock.Verify(repo => repo.Add(It.IsAny<WorkflowTemplate>()), Times.Once);
        tenantMock.Verify(tenant => tenant.Commit(), Times.Once);
    }
}