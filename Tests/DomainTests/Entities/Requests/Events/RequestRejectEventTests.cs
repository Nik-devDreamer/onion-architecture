using Domain.Entities.Requests.Events;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests.Entities.Requests.Events
{
    [TestFixture]
    class RequestRejectEventTests
    {
        [Test]
        public void Constructor_ValidParameters_ObjectCreatedTest()
        {
            // Arrange
            Guid requestId = Guid.NewGuid();

            // Act
            var requestRejectEvent = new RequestRejectEvent(requestId);

            // Assert
            requestRejectEvent.Should().NotBeNull();
            requestRejectEvent.Id.Should().NotBe(Guid.Empty);
            requestRejectEvent.RequestId.Should().Be(requestId);
            requestRejectEvent.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}
