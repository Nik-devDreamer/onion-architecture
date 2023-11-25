using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.Entities.Requests;
using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class RequestProgressTests
    {
        private Fixture _fixture;

        public RequestProgressTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Workflow_Constructor_ShouldSetProperties()
        {
            // Arrange
            var workflowTemplateId = Guid.NewGuid();
            var workflowName = "Test Workflow";
            var steps = new List<WorkflowStep>
            {
                new WorkflowStep("Step1", 1, Guid.NewGuid(), Guid.NewGuid(), "Comment1"),
                new WorkflowStep("Step2", 2, Guid.NewGuid(), Guid.NewGuid(), "Comment2"),
                new WorkflowStep("Step3", 3, Guid.NewGuid(), Guid.NewGuid(), "Comment3")
            };

            // Act
            var workflow = new Workflow(workflowTemplateId, workflowName, steps);

            // Assert
            workflow.WorkflowTemplateId.Should().Be(workflowTemplateId);
            workflow.Name.Should().Be(workflowName);
            workflow.Steps.Should().BeEquivalentTo(steps);
        }

        [Test]
        public void AdvanceStep_WhenCalled_ShouldIncrementCurrentStep()
        {
            // Arrange
            var requestProgress = _fixture.Create<RequestProgress>();
            var currentStep = _fixture.Create<int>();

            var fakeStep = new WorkflowStep(
                _fixture.Create<string>(),
                currentStep,
                _fixture.Create<Guid?>(),
                _fixture.Create<Guid?>(),
                _fixture.Create<string>()
            );

            // Act
            requestProgress.AdvanceStep(fakeStep);

            // Assert
            requestProgress.CurrentStep.Should().Be(1);
        }

        [Test]
        public void Reject_WhenCalledWithIncorrectUser_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var requestProgress = _fixture.Create<RequestProgress>();
            var correctUserId = _fixture.Create<Guid>();
            var incorrectUserId = _fixture.Create<Guid>();

            var fakeStep = new WorkflowStep(
                _fixture.Create<string>(),
                _fixture.Create<int>(),
                correctUserId,
                _fixture.Create<Guid?>(),
                _fixture.Create<string>()
            );

            requestProgress.AdvanceStep(fakeStep);

            // Act
            Action rejectAction = () => requestProgress.Reject(incorrectUserId);

            // Assert
            rejectAction.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Reject_ShouldThrowException_WhenUserIdDoesNotMatch()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToList());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            Action act = () => requestProgress.Reject(Guid.NewGuid());

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("User does not have permission to perform this action.");
        }
        
        [Test]
        public void Approve_ShouldSetIsApprovedToTrue_WhenUserIdMatches()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
                ).ToList());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;

            // Act
            requestProgress.Approve(id);

            // Assert
            requestProgress.IsApproved.Should().BeTrue();
        }

        [Test]
        public void Reject_ShouldSetIsRejectedToTrue_WhenUserIdMatches()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToList());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;

            // Act
            requestProgress.Reject(id);

            // Assert
            requestProgress.IsRejected.Should().BeTrue();
        }
        
        [Test]
        public void Approve_ShouldSetIsApproved_WhenUserIdMatches()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToList());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;

            // Act
            requestProgress.Approve(userId);

            // Assert
            requestProgress.IsApproved.Should().BeTrue();
            requestProgress.IsRejected.Should().BeFalse();
        }

        [Test]
        public void Approve_ShouldThrowException_WhenUserIdDoesNotMatch()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToList());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;
            userId = Guid.NewGuid();

            // Act
            Action act = () => requestProgress.Approve(userId);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("User does not have permission to perform this action.");
            requestProgress.IsApproved.Should().BeFalse();
            requestProgress.IsRejected.Should().BeFalse();
        }
    }
}