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
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var step1 = new WorkflowStepTemplate("Step1", 1, userId, roleId);
            var step2 = new WorkflowStepTemplate("Step2", 2, userId, roleId);

            var steps = new WorkflowStepTemplate[] { step1, step2 };
            var workflowTemplate = WorkflowTemplate.Create(workflowTemplateName, steps);

            // Act
            var workflow = Workflow.Create(workflowTemplate);

            // Assert
            workflow.Should().NotBeNull();
            workflow.WorkflowTemplateId.Should().NotBe(Guid.Empty);
            workflow.Name.Should().Be(workflowTemplateName);
            workflow.Steps.Should().NotBeNull().And.HaveCount(2);
            workflow.Steps.ElementAt(0).Name.Should().Be("Step1");
            workflow.Steps.ElementAt(0).Order.Should().Be(1);
            workflow.Steps.ElementAt(0).UserId.Should().Be(userId);
            workflow.Steps.ElementAt(0).RoleId.Should().Be(roleId);
            workflow.Steps.ElementAt(1).Name.Should().Be("Step2");
            workflow.Steps.ElementAt(1).Order.Should().Be(2);
            workflow.Steps.ElementAt(1).UserId.Should().Be(userId);
            workflow.Steps.ElementAt(1).RoleId.Should().Be(roleId);
        }
    }
}