using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.BaseObjectsNamespace;
using onion_architecture.Domain.Entities.Requests;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class DocumentTests
    {
        [Test]
        public void Document_CanBeCreatedWithValidEmail()
        {
            // Arrange
            var emailValue = "test@gmail.com";
            var email = new Email(emailValue);

            // Act
            var document = new Document(email);

            // Assert
            document.Should().NotBeNull();
            document.Email.Value.Should().Be(emailValue);
        }

        [Test]
        public void Document_ShouldThrowExceptionWhenNullEmailIsPassed()
        {
            // Arrange
            Email nullEmail = null;

            // Act
            Action act = () => new Document(nullEmail);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
