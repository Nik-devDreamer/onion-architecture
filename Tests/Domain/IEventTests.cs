using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.Entities.Requests.Events;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class IEventTests
    {
        private Fixture _fixture;

        public IEventTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Constructor_ValidGUID_ShouldSetCorrectValues()
        {
            // Arrange
            var validGUID = _fixture.Create<Guid>();

            // Act
            var requestCreateEvent = new RequestCreateEvent(validGUID);

            // Assert
            requestCreateEvent.RequestId.Should().Be(validGUID);
            requestCreateEvent.Id.Should().NotBe(Guid.Empty);
            requestCreateEvent.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}
