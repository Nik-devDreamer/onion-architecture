using Application.Requests.Commands;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class RestartRequestCommandTests
{
    [Test]
    public void Constructor_ValidRequestId_PropertiesSetCorrectlyTest()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var command = new RestartRequestCommand(requestId);

        // Assert
        Assert.That(command.RequestId, Is.EqualTo(requestId));
    }

    [Test]
    public void Constructor_EmptyRequestId_ThrowsArgumentExceptionTest()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new RestartRequestCommand(Guid.Empty));
    }
}