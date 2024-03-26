using Application.Requests.Commands;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class ApproveRequestCommandTests
{
    [Test]
    public void Constructor_ValidArguments_PropertiesSetCorrectlyTest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        // Act
        var command = new ApproveRequestCommand(userId, requestId);

        // Assert
        Assert.That(command.UserId, Is.EqualTo(userId));
        Assert.That(command.RequestId, Is.EqualTo(requestId));
    }

    [Test]
    public void Constructor_EmptyUserId_ThrowsArgumentExceptionTest()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ApproveRequestCommand(Guid.Empty, requestId));
    }

    [Test]
    public void Constructor_EmptyRequestId_ThrowsArgumentExceptionTest()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ApproveRequestCommand(userId, Guid.Empty));
    }
}