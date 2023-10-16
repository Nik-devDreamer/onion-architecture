using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using Onion_architecture.Domain.BaseObjectsNamespace;

namespace Onion_architecture.Tests
{
    [TestFixture]
    class EmailTests
    {
        private Fixture _fixture;

        public EmailTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Constructor_ValidEmail_ShouldNotThrowException()
        {
            // Arrange
            string validEmail = _fixture.Create<string>() + "@gmail.com";

            // Act
            Action act = () => new Email(validEmail);

            // Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Constructor_InvalidEmail_ShouldThrowArgumentException()
        {
            // Arrange
            string invalidEmail = "invalidEmail";

            // Act
            Action act = () => new Email(invalidEmail);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid email");
        }

        [Test]
        public void Constructor_NullEmail_ShouldThrowArgumentException()
        {
            // Arrange
            string nullEmail = null;

            // Act
            Action act = () => new Email(nullEmail);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid email");
        }

        [Test]
        public void Constructor_EmptyEmail_ShouldThrowArgumentException()
        {
            // Arrange
            string emptyEmail = string.Empty;

            // Act
            Action act = () => new Email(emptyEmail);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid email");
        }
    }
}