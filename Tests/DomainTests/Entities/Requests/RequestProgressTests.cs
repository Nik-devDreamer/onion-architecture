using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using Domain.Entities.Users;
using Domain.Entities.WorkflowTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests.Entities.Requests
{
    [TestFixture]
    class RequestProgressTests
    {
        private Fixture _fixture;

        public RequestProgressTests()
        {
            _fixture = new Fixture();
        }
        
        private User CreateFakeUser()
        {
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");
            return new User(id, name, email, roleId, password);
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
            var correctUser = CreateFakeUser();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, correctUser.Id, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);
            
            // Act
            requestProgress.AdvanceStep(workflow.Steps.ElementAt(0),correctUser);

            // Assert
            requestProgress.CurrentStep.Should().Be(1);
        }

        [Test]
        public void Reject_WhenCalledWithIncorrectUser_ShouldThrowInvalidOperationExceptionTest()
        {
            // Arrange
            var correctUser = CreateFakeUser();
            var incorrectUser = CreateFakeUser();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, correctUser.Id, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            Action rejectAction = () => requestProgress.Reject(incorrectUser);

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
            var incorrectUser = CreateFakeUser();

            // Act
            Action act = () => requestProgress.Reject(incorrectUser);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("User does not have permission to perform this action.");
        }
        
        [Test]
        public void Approve_ShouldSetIsApprovedToTrue_WhenUserIdMatchesTest()
        {
            // Arrange
            var correctUser = CreateFakeUser();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, correctUser.Id, Guid.NewGuid(), "Comment1")
                ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            requestProgress.Approve(correctUser);

            // Assert
            requestProgress.IsApproved.Should().BeTrue();
        }

        [Test]
        public void Reject_ShouldSetIsRejectedToTrue_WhenUserIdMatchesTest()
        {
            // Arrange
            var correctUser = CreateFakeUser();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, correctUser.Id, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            requestProgress.Reject(correctUser);

            // Assert
            requestProgress.IsRejected.Should().BeTrue();
        }
        
        [Test]
        public void Approve_ShouldSetIsApproved_WhenUserIdMatchesTest()
        {
            // Arrange
            var correctUser = CreateFakeUser();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, correctUser.Id, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            requestProgress.Approve(correctUser);

            // Assert
            requestProgress.IsApproved.Should().BeTrue();
            requestProgress.IsRejected.Should().BeFalse();
        }

        [Test]
        public void Approve_ShouldThrowException_WhenUserIdDoesNotMatchTest()
        {
            // Arrange
            var correctUser = CreateFakeUser();
            var requestId = Guid.NewGuid();
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();
            var workflow = new Workflow(workflowTemplate.Id, workflowTemplate.Name, workflowTemplate.Steps.Select(
                step => new WorkflowStep("Step1", 1, correctUser.Id, Guid.NewGuid(), "Comment1")
            ).ToArray());
            var requestProgress = new RequestProgress(requestId, workflow);

            // Act
            Action act = () => requestProgress.Approve(correctUser);

            // Assert
            requestProgress.IsApproved.Should().BeFalse();
            requestProgress.IsRejected.Should().BeFalse();
        }
    }
}