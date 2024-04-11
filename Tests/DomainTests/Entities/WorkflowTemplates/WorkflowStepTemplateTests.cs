using AutoFixture;
using Domain.Entities.WorkflowTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests.Entities.WorkflowTemplates
{
    [TestFixture]
    class WorkflowStepTemplateTests
    {
        private Fixture _fixture;

        public WorkflowStepTemplateTests()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void Constructor_ShouldSetPropertiesCorrectlyTest()
        {
            // Arrange
            string name = _fixture.Create<string>();
            int order = _fixture.Create<int>();
            Guid? userId = _fixture.Create<Guid>();
            Guid? roleId = _fixture.Create<Guid>();

            // Act
            var template = new WorkflowStepTemplate(name, order, userId, roleId);

            // Assert
            template.Name.Should().Be(name);
            template.Order.Should().Be(order);
            template.UserId.Should().Be(userId);
            template.RoleId.Should().Be(roleId);
        }

        [Test]
        public void Constructor_NullName_ShouldThrowArgumentNullExceptionTest()
        {
            // Arrange
            string name = null;
            int order = _fixture.Create<int>();
            Guid? userId = _fixture.Create<Guid>();
            Guid? roleId = _fixture.Create<Guid>();

            // Act
            Action action = () => new WorkflowStepTemplate(name, order, userId, roleId);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("*name*");
        }
    }
}