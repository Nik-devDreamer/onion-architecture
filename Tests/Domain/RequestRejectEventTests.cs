using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.Entities.Requests.Events;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class RequestRejectEventTests
    {
        [Test]
        public void Constructor_ValidParameters_ObjectCreated()
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
