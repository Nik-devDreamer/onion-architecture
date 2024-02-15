using Domain.Entities.Requests.Events;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests;

[TestFixture]
public class RequestCreateEventTests
{
    [Test]
    public void RequestCreateEvent_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var requestCreateEvent = new RequestCreateEvent(requestId);

        // Assert
        Assert.AreEqual(requestId, requestCreateEvent.RequestId);
        Assert.AreNotEqual(Guid.Empty, requestCreateEvent.Id);
        Assert.That(requestCreateEvent.Date, Is.EqualTo(DateTime.UtcNow).Within(1).Seconds);
    }

    [Test]
    public void RequestCreateEvent_Constructor_DoesNotThrowException()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act & Assert
        Assert.DoesNotThrow(() => new RequestCreateEvent(requestId));
    }
}