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
    class WorkflowTemplateTests
    {
        private Fixture _fixture;

        public WorkflowTemplateTests()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void Create_ValidParameters_ReturnsValidWorkflowTemplate()
        {
            // Arrange
            Guid templateId = Guid.NewGuid();
            string templateName = "Interview Process";
            var steps = new List<WorkflowStepTemplate>
            {
                new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
                new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid()),
            };

            // Act
            var workflowTemplate = new WorkflowTemplate(templateId, templateName, steps);

            // Assert
            workflowTemplate.Id.Should().Be(templateId);
            workflowTemplate.Name.Should().Be(templateName);
            workflowTemplate.Steps.Should().NotBeNull();
            workflowTemplate.Steps.Should().HaveCount(steps.Count);
            workflowTemplate.Steps.Select(s => s.Name).Should().BeEquivalentTo(steps.Select(s => s.Name));
            workflowTemplate.Steps.Select(s => s.Order).Should().BeEquivalentTo(steps.Select(s => s.Order));
        }
        
        [Test]
        public void Create_ShouldCreateRequestWithCorrectValues()
        {
            // Arrange
            var workflowTemplateId = Guid.NewGuid();
            var workflowTemplateName = "TestWorkflowTemplate";
            var workflowStepTemplates = _fixture.CreateMany<WorkflowStepTemplate>().ToList();

            var workflowTemplate = new WorkflowTemplate(workflowTemplateId, workflowTemplateName, workflowStepTemplates);
            var userName = "John";
            var userEmail = _fixture.Create<string>() + "@gmail.com";
            var userRole = _fixture.Create<Role>();
            var validPassword = new Password("Test@123");
            var userPassword = validPassword;
            var user = User.Create(userName, new Email(userEmail), userRole, userPassword);

            var documentEmail = _fixture.Create<string>() + "@gmail.com";
            var document = new Document(new Email(documentEmail));
            var comment = "Test Comment";

            // Act
            var request = workflowTemplate.Create(user, document, comment);

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