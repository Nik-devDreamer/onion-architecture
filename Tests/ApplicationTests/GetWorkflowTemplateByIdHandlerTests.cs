using Application.Repositories;
using Application.WorkflowTemplates.Handlers;
using Application.WorkflowTemplates.Queries;
using Domain.Entities.WorkflowTemplates;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class GetWorkflowTemplateByIdHandlerTests
{
    [Test]
    public void Handle_ReturnsCorrectWorkflowTemplate_WhenValidIdIsProvidedTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var tenantMock = new Mock<ITenant>();
        var workflowRepositoryMock = new Mock<IWorkflowTemplateRepository>();

        var handler = new GetWorkflowTemplateByIdHandler(tenantFactoryMock.Object);
        var query = new GetWorkflowTemplateByIdQuery(Guid.NewGuid());

        var expectedWorkflowTemplate = new WorkflowTemplate(Guid.NewGuid(), "Workflow 1", new WorkflowStepTemplate[0]);

        tenantFactoryMock.Setup(factory => factory.GetTenant()).Returns(tenantMock.Object);
        tenantMock.Setup(tenant => tenant.WorkflowsTemplate).Returns(workflowRepositoryMock.Object);
        workflowRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(expectedWorkflowTemplate);

        // Act
        var result = handler.Handle(query);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(expectedWorkflowTemplate));
    }

    [Test]
    public void Handle_ThrowsArgumentException_WhenEmptyIdIsProvidedTest()
    {
        // Arrange
        var tenantFactoryMock = new Mock<ITenantFactory>();
        var handler = new GetWorkflowTemplateByIdHandler(tenantFactoryMock.Object);
        var query = new GetWorkflowTemplateByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => handler.Handle(query));
    }
}