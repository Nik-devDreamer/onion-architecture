using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.BaseObjectsNamespace;
using onion_architecture.Domain.Entities.Requests;
using onion_architecture.Domain.Entities.Requests.Events;
using onion_architecture.Domain.Entities.Users;
using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class RequestTests
    {
        private Fixture _fixture;

        public RequestTests()
        {
            _fixture = new Fixture();
        }

        private Request CreateRequest()
        {
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var role = new Role("TestRole");
            var password = new Password("Test@123");
            var document = new Document(email);
            var user = User.Create(name, email, role, password);
            List<WorkflowStepTemplate> steps = CreateDefaultSteps(user.Id, user.RoleId);
            WorkflowTemplate workflowTemplate = new WorkflowTemplate(Guid.NewGuid(), "HR", steps);
            return workflowTemplate.Create(user, document, "Comment");
        }

        private static List<WorkflowStepTemplate> CreateDefaultSteps(Guid userId, Guid roleGuid)
        {
            return new List<WorkflowStepTemplate>
            {
                new WorkflowStepTemplate("Online Interview", 1, userId, roleGuid),
                new WorkflowStepTemplate("Interview with HR", 2, userId, roleGuid),
                new WorkflowStepTemplate("Technical Task", 3, userId, roleGuid),
                new WorkflowStepTemplate("Meeting with CEO", 4, userId, roleGuid),
            };
        }

        private void ProgressApprove(Request request)
        {
            while (request.Progress.CurrentStep < request.Workflow.Steps.Count)
            {
                request.Approve();
            }
        }

        private void ProgressReject(Request request)
        {
            while (request.Progress.CurrentStep < request.Workflow.Steps.Count)
            {
                request.Reject();
            }
        }

        [Test]
        public void Restart_ShouldResetProgress_WhenCalled()
        {
            // Arrange
            var request = CreateRequest();
            request.Approve();

            // Act
            request.Restart();

            // Assert
            request.IsApproved().Should().BeFalse();
            request.IsRejected().Should().BeFalse();
            request.Progress.CurrentStep.Should().Be(0);
        }

        [Test]
        public void Approve_ShouldApproveRequest_WhenValid()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            ProgressApprove(request);

            // Assert
            request.IsApproved().Should().BeTrue();
            request.IsRejected().Should().BeFalse();
            request.EventsList.Should().ContainSingle(e => e is RequestApprovedEvent);
        }

        [Test]
        public void Reject_ShouldRejectRequest_WhenValid()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            ProgressReject(request);

            // Assert
            request.IsRejected().Should().BeTrue();
            request.IsApproved().Should().BeFalse();
            request.EventsList.Should().ContainSingle(e => e is RequestRejectEvent);
        }

        [Test]
        public void EventsList_ShouldContainEvents_WhenActionsPerformed()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            ProgressApprove(request);

            // Assert
            request.EventsList.Should().NotBeEmpty().And.ContainItemsAssignableTo<IEvent>();
        }
        
        [Test]
        public void Approve_Should_ThrowException_When_NoNextStep()
        {
            // Arrange
            var request = CreateRequest();
            ProgressApprove(request);

            // Act & Assert
            Action act = () => request.Approve();
            act.Should().Throw<InvalidOperationException>().WithMessage("No next step is available");
        }

        [Test]
        public void Reject_Should_ThrowException_When_NoNextStep()
        {
            // Arrange
            var request = CreateRequest();
            ProgressApprove(request);

            // Act & Assert
            Action act = () => request.Reject();
            act.Should().Throw<InvalidOperationException>().WithMessage("No next step is available");
        }
    }
}