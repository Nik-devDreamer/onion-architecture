using Application.Repositories;
using Application.WorkflowTemplates.Handlers;
using Application.WorkflowTemplates.Queries;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class GetAllWorkflowTemplatesHandlerTests
{
    [Test]
    public void Handle_ReturnsAllWorkflowTemplates_WhenCalledTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var workflowRepositoryMock = new Mock<IWorkflowTemplateRepository>();

        var handler = new GetAllWorkflowTemplatesHandler(tenantFactoryMock.Object);
        var query = new GetAllWorkflowTemplatesQuery();

        var workflowTemplates = new List<WorkflowTemplate>
        {
            new WorkflowTemplate(Guid.NewGuid(), "Workflow 1", new WorkflowStepTemplate[0]),
            new WorkflowTemplate(Guid.NewGuid(), "Workflow 2", new WorkflowStepTemplate[0])
        };

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.WorkflowsTemplate).Returns(workflowRepositoryMock.Object);
        workflowRepositoryMock.Setup(repo => repo.GetAll()).Returns(workflowTemplates);

        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(workflowTemplates.Count));
    
        foreach (var template in workflowTemplates)
        {
            Assert.IsTrue(result.Any(t => t.Id == template.Id && t.Name == template.Name));
        }
    }

    [Test]
    public void Handle_ReturnsEmptyCollection_WhenNoWorkflowTemplatesExistTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var workflowRepositoryMock = new Mock<IWorkflowTemplateRepository>();

        var handler = new GetAllWorkflowTemplatesHandler(tenantFactoryMock.Object);
        var query = new GetAllWorkflowTemplatesQuery();

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.WorkflowsTemplate).Returns(workflowRepositoryMock.Object);
        workflowRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<WorkflowTemplate>());
        
        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }
}