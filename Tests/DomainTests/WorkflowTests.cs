using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.Entities.Requests;

namespace onion_architecture.Tests.Domain
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
            var step1 = WorkflowStep.Create("Step1", 1, Guid.NewGuid(), Guid.NewGuid(), "Comment1");
            var step2 = WorkflowStep.Create("Step2", 2, Guid.NewGuid(), Guid.NewGuid(), "Comment2");

            var steps = new WorkflowStep[] { step1, step2 };

            // Act
            var workflow = Workflow.Create(workflowTemplateName, steps);

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