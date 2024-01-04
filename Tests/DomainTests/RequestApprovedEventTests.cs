using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.Entities.Requests.Events;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class RequestApprovedEventTests
    {
        [Test]
        public void Constructor_ValidParameters_ObjectCreatedTest()
        {
            // Arrange
            var requestId = Guid.NewGuid();

            // Act
            var requestApprovedEvent = new RequestApprovedEvent(requestId);

            // Assert
            requestApprovedEvent.Should().NotBeNull();
            requestApprovedEvent.Id.Should().NotBeEmpty();
            requestApprovedEvent.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            requestApprovedEvent.RequestId.Should().Be(requestId);
        }
    }
}