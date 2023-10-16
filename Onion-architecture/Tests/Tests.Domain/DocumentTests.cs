using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using Onion_architecture.Domain.BaseObjectsNamespace;
using Onion_architecture.Domain.Entities.Requests;

namespace Onion_architecture.Tests.Tests.Domain
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
            Assert.NotNull(document);
            Assert.AreEqual(emailValue, document.Email.Value);
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
