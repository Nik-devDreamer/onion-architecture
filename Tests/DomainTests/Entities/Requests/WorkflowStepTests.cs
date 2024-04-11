using AutoFixture;
using Domain.Entities.Requests;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests.Entities.Requests
{
    [TestFixture]
    class WorkflowStepTests
    {
        private Fixture _fixture;

        public WorkflowStepTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Constructor_ShouldSetPropertiesCorrectlyTest()
        {
            // Arrange
            string name = "Interview";
            int order = 1;
            Guid userId = Guid.NewGuid();
            Guid roleId = Guid.NewGuid();
            string comment = "Initial step";

            // Act
            WorkflowStep workflowStep = new WorkflowStep(name, order, userId, roleId, comment);

            // Assert
            workflowStep.Name.Should().Be(name);
            workflowStep.Order.Should().Be(order);
            workflowStep.UserId.Should().Be(userId);
            workflowStep.RoleId.Should().Be(roleId);
            workflowStep.Comment.Should().Be(comment);
        }

        [Test]
        public void UpdateComment_ShouldUpdateCommentCorrectlyTest()
        {
            // Arrange
            WorkflowStep workflowStep = _fixture.Create<WorkflowStep>();
            string newComment = "Updated comment";

            // Act
            workflowStep.UpdateComment(newComment);

            // Assert
            workflowStep.Comment.Should().Be(newComment);
        }

        [TestCaseSource(nameof(WorkflowStepTestData))]
        public void Create_ShouldCreateWorkflowStepWithCorrectProperties(string name, int order, Guid? userId, Guid? roleId, string comment)
        {
            // Act
            var workflowStep = WorkflowStep.Create(name, order, userId, roleId, comment);

            // Assert
            Assert.That(workflowStep.Name, Is.EqualTo(name));
            Assert.That(workflowStep.Order, Is.EqualTo(order));
            Assert.That(workflowStep.UserId, Is.EqualTo(userId));
            Assert.That(workflowStep.RoleId, Is.EqualTo(roleId));
            Assert.That(workflowStep.Comment, Is.EqualTo(comment));
        }

        private static IEnumerable<TestCaseData> WorkflowStepTestData()
        {
            yield return new TestCaseData("Interview", 1, null, null, null).SetName("Create_ShouldCreateWorkflowStepWithAllNullProperties");
            yield return new TestCaseData("Interview", 1, null, null, "Initial step").SetName("Create_ShouldCreateWorkflowStepWithInitialStepComment");
            yield return new TestCaseData("Interview", 1, Guid.NewGuid(), null, null).SetName("Create_ShouldCreateWorkflowStepWithUserId");
            yield return new TestCaseData("Interview", 1, null, Guid.NewGuid(), null).SetName("Create_ShouldCreateWorkflowStepWithRoleId");
            yield return new TestCaseData("Interview", 1, null, null, "Initial step").SetName("Create_ShouldCreateWorkflowStepWithInitialStepComment");
        }
    }
}