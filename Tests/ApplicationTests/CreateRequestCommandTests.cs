using Application.Requests.Commands;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class CreateRequestCommandTests
{
    private Document CreateDocument()
    {
        var email = new Email("test@gmail.com");
        var name = "testName";
        var phoneNumber = "+1234567890";
        var dateOfBirth = new DateTime(2000, 2, 2);
        return new Document(email, name, phoneNumber, dateOfBirth);
    }
    
    [Test]
    public void Constructor_ValidArguments_PropertiesSetCorrectlyTest()
    {
        // Arrange
        var document = CreateDocument();
        var userId = Guid.NewGuid();
        var workflowTemplateId = Guid.NewGuid();

        // Act
        var command = new CreateRequestCommand(userId, document, workflowTemplateId);

        // Assert
        Assert.That(command.UserId, Is.EqualTo(userId));
        Assert.That(command.Document, Is.EqualTo(document));
        Assert.That(command.WorkflowTemplateId, Is.EqualTo(workflowTemplateId));
    }

    [Test]
    public void Constructor_EmptyUserId_ThrowsArgumentExceptionTest()
    {
        // Arrange
        var document = CreateDocument();
        var workflowTemplateId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CreateRequestCommand(Guid.Empty, document, workflowTemplateId));
    }

    [Test]
    public void Constructor_NullDocument_ThrowsArgumentNullExceptionTest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var workflowTemplateId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateRequestCommand(userId, null, workflowTemplateId));
    }

    [Test]
    public void Constructor_EmptyWorkflowTemplateId_ThrowsArgumentExceptionTest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var document = CreateDocument();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CreateRequestCommand(userId, document, Guid.Empty));
    }
}