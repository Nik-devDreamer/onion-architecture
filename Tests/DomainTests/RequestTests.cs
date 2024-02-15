using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using Domain.Entities.Requests.Events;
using Domain.Entities.Users;
using Domain.Entities.WorkflowTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests
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
            var document = new Document(email, name, "1234567890", DateTime.Now);
            var user = User.Create(name, email, role, password);
            List<WorkflowStepTemplate> steps = CreateDefaultSteps(user.Id, role.Id);
            WorkflowTemplate workflowTemplate = new WorkflowTemplate(Guid.NewGuid(), "HR", steps.ToArray());
            return workflowTemplate.CreateRequest(user, document, "Comment");
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

        [Test]
        public void Restart_ShouldResetProgress_WhenCalledTest()
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
        public void Approve_ShouldApproveRequest_WhenValidTest()
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
        public void Reject_ShouldRejectRequest_WhenValidTest()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            request.Reject();

            // Assert
            request.IsRejected().Should().BeTrue();
            request.IsApproved().Should().BeFalse();
            request.EventsList.Should().ContainSingle(e => e is RequestRejectEvent);
        }

        [Test]
        public void EventsList_ShouldContainEvents_WhenActionsPerformedTest()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            ProgressApprove(request);

            // Assert
            request.EventsList.Should().NotBeEmpty().And.ContainItemsAssignableTo<IEvent>();
        }
        
        [Test]
        public void Approve_Should_ThrowException_When_NoNextStepTest()
        {
            // Arrange
            var request = CreateRequest();
            ProgressApprove(request);

            // Act & Assert
            Action act = () => request.Approve();
            act.Should().Throw<InvalidOperationException>().WithMessage("Cannot advance to the next step: either the request is already approved or rejected, or there are no more steps available.");
        }

        [Test]
        public void Reject_ShouldThrowException_WhenAlreadyApprovedTest()
        {
            // Arrange
            var request = CreateRequest();
            ProgressApprove(request);

            // Act & Assert
            Action act = () => request.Reject();
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Request is already approved or rejected");
        }
    }
}