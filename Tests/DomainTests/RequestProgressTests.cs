using AutoFixture;
using Domain.Entities.Requests;
using Domain.Entities.WorkflowTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests
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
        public void Workflow_Constructor_ShouldSetPropertiesTest()
        {
            // Arrange
            var workflowTemplateId = Guid.NewGuid();
            var workflowName = "Test Workflow";
            var steps = new WorkflowStep[]
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
        public void AdvanceStep_WhenCalled_ShouldIncrementCurrentStepTest()
        {
            // Arrange
            var requestProgress = _fixture.Create<RequestProgress>();
            var currentStep = _fixture.Create<int>();

            var fakeStep = WorkflowStep.Create("Step1", 1, Guid.NewGuid(), Guid.NewGuid(), "Comment1");

            // Act
            requestProgress.AdvanceStep(fakeStep, fakeStep.UserId);

            // Assert
            requestProgress.CurrentStep.Should().Be(1);
        }

        [Test]
        public void Reject_WhenCalledWithIncorrectUser_ShouldThrowInvalidOperationExceptionTest()
        {
            // Arrange
            var requestProgress = _fixture.Create<RequestProgress>();
            var correctUserId = _fixture.Create<Guid>();
            var incorrectUserId = _fixture.Create<Guid>();

            var fakeStep = WorkflowStep.Create("Step1", 1, Guid.NewGuid(), Guid.NewGuid(), "Comment1");

            requestProgress.AdvanceStep(fakeStep, fakeStep.UserId);

            // Act
            Action rejectAction = () => requestProgress.Reject(incorrectUserId);

            // Assert
            rejectAction.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Reject_ShouldThrowException_WhenUserIdDoesNotMatchTest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            Action act = () => requestProgress.Reject(Guid.NewGuid());

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("User does not have permission to perform this action.");
        }
        
        [Test]
        public void Approve_ShouldSetIsApprovedToTrue_WhenUserIdMatchesTest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
                ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;

            // Act
            requestProgress.Approve(id);

            // Assert
            requestProgress.IsApproved.Should().BeTrue();
        }

        [Test]
        public void Reject_ShouldSetIsRejectedToTrue_WhenUserIdMatchesTest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;

            // Act
            requestProgress.Reject(id);

            // Assert
            requestProgress.IsRejected.Should().BeTrue();
        }
        
        [Test]
        public void Approve_ShouldSetIsApproved_WhenUserIdMatchesTest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);
            var id = workflow.Steps[requestProgress.CurrentStep].UserId;

            // Act
            requestProgress.Approve(userId);

            // Assert
            requestProgress.IsApproved.Should().BeTrue();
            requestProgress.IsRejected.Should().BeFalse();
        }

        [Test]
        public void Approve_ShouldThrowException_WhenUserIdDoesNotMatchTest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, userId, Guid.NewGuid(), "Comment1")
            ).ToArray());
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