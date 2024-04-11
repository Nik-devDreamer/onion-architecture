using AutoFixture;
using Domain.BaseObjectsNamespace;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests.BaseObjectsNamespace
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
        public void Constructor_InvalidEmail_ShouldThrowArgumentExceptionTest()
        {
            // Arrange
            string invalidEmail = "invalidEmail";

            // Act
            Action act = () => new Email(invalidEmail);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid email");
        }

        [Test]
        public void Constructor_NullEmail_ShouldThrowArgumentExceptionTest()
        {
            // Arrange
            string nullEmail = null;

            // Act
            Action act = () => new Email(nullEmail);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid email");
        }

        [Test]
        public void Constructor_EmptyEmail_ShouldThrowArgumentExceptionTest()
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