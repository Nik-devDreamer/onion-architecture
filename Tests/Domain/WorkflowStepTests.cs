using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.Entities.Requests;

namespace onion_architecture.Tests.Domain
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
        public void Constructor_ShouldSetPropertiesCorrectly()
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
        public void UpdateComment_ShouldUpdateCommentCorrectly()
        {
            // Arrange
            WorkflowStep workflowStep = _fixture.Create<WorkflowStep>();
            string newComment = "Updated comment";

            // Act
            workflowStep.UpdateComment(newComment);

            // Assert
            workflowStep.Comment.Should().Be(newComment);
        }
    }
}