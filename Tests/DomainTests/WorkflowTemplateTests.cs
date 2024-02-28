using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using Domain.Entities.Users;
using Domain.Entities.WorkflowTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests
{
    [TestFixture]
    class WorkflowTemplateTests
    {
        private Fixture _fixture;

        public WorkflowTemplateTests()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void Create_ValidParameters_ReturnsValidWorkflowTemplateTest()
        {
            // Arrange
            Guid templateId = Guid.NewGuid();
            string templateName = "Interview Process";
            var steps = new WorkflowStepTemplate[]
            {
                new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
                new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid())
            };

            // Act
            var workflowTemplate = new WorkflowTemplate(templateId, templateName, steps);

            // Assert
            workflowTemplate.Id.Should().Be(templateId);
            workflowTemplate.Name.Should().Be(templateName);
            workflowTemplate.Steps.Should().NotBeNull();
            workflowTemplate.Steps.Should().HaveCount(steps.Length);
            workflowTemplate.Steps.Select(s => s.Name).Should().BeEquivalentTo(steps.Select(s => s.Name));
            workflowTemplate.Steps.Select(s => s.Order).Should().BeEquivalentTo(steps.Select(s => s.Order));
        }
        
        [Test]
        public void Create_ShouldCreateRequestWithCorrectValuesTest()
        {
            // Arrange
            var workflowTemplateName = "TestWorkflowTemplate";
            var stepTemplates = new WorkflowStepTemplate[]
            {
                new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
                new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid())
            };
            var workflowTemplate = WorkflowTemplate.Create(workflowTemplateName, stepTemplates);
    
            var userName = "John";
            var userEmail = _fixture.Create<string>() + "@gmail.com";
            var userRole = _fixture.Create<Role>();
            var validPassword = new Password("Test@123");
            var userPassword = validPassword;
            var user = User.Create(userName, new Email(userEmail), userRole, userPassword);

            var documentEmail = _fixture.Create<string>() + "@gmail.com";
            var document = new Document(new Email(documentEmail), "John Doe", "123456789", DateTime.Now);
            var comment = "Test Comment";

            // Act
            var request = workflowTemplate.CreateRequest(user, document);

            // Assert
            request.Should().NotBeNull();
            request.Id.Should().NotBe(Guid.Empty);
            request.UserId.Should().Be(user.Id);
            request.Document.Should().Be(document);
            request.Workflow.Should().NotBeNull();
            request.Progress.Should().NotBeNull();
            request.Progress.RequestId.Should().Be(request.Id);
            request.Progress.CurrentStep.Should().Be(0);
            request.Progress.IsApproved.Should().BeFalse();
            request.Progress.IsRejected.Should().BeFalse();
        }
    }
}