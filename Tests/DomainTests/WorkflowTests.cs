using AutoFixture;
using Domain.Entities.Requests;
using Domain.Entities.WorkflowTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests
{
    [TestFixture]
    class WorkflowTests
    {
        private Fixture _fixture;

        public WorkflowTests()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void Create_ShouldCreateWorkflowWithCorrectValuesTest()
        {
            // Arrange
            var workflowTemplateName = "TestWorkflowTemplate";
            var step1 = new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid());
            var step2 = new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid());

            var steps = new WorkflowStepTemplate[] { step1, step2 };
            var workflowTemplate = WorkflowTemplate.Create(workflowTemplateName, steps);

            // Act
            var workflow = Workflow.Create(workflowTemplate);

            // Assert
            workflow.Should().NotBeNull();
            workflow.WorkflowTemplateId.Should().NotBe(Guid.Empty);
            workflow.Name.Should().Be(workflowTemplateName);
            workflow.Steps.Should().NotBeNull().And.HaveCount(2);
            workflow.Steps[0].Should().BeEquivalentTo(step1);
            workflow.Steps[1].Should().BeEquivalentTo(step2);
        }
    }
}