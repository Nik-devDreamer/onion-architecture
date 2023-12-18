﻿using System;
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
        public void Document_CanBeCreatedWithValidEmailTest()
        {
            // Arrange
            var email = new Email("test@gmail.com");
            var name = "testName";
            var phoneNumber = "+1234567890";
            var dateOfBirth = new DateTime(2000, 2, 2);

            // Act
            var document = new Document(email, name, phoneNumber, dateOfBirth);

            // Assert
            document.Should().NotBeNull();
            document.Email.Value.Should().Be("test@gmail.com");
            document.Name.Should().Be(name);
            document.PhoneNumber.Should().Be(phoneNumber);
            document.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Test]
        public void Document_ShouldThrowExceptionWhenNullEmailIsPassedTest()
        {
            // Arrange
            Email nullEmail = null;
            var name = "testName";
            var phoneNumber = "+1234567890";
            var dateOfBirth = new DateTime(2000, 2, 2);

            // Act
            Action act = () => new Document(nullEmail, name, phoneNumber, dateOfBirth);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
