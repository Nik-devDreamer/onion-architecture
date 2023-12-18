﻿using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using onion_architecture.Domain.BaseObjectsNamespace;
using System.Text.RegularExpressions;

namespace onion_architecture.Tests.Domain
{
    [TestFixture]
    class PasswordTests
    {
        private Fixture _fixture;
        private const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&+=]).*$";

        public PasswordTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Password_ValueShouldBeSetTest()
        {
            // Arrange
            var testingPassword = _fixture.Create<string>();

            // Act
            Action act = () => new Password(testingPassword);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid password");
        }

        [Test]
        public void Password_ShouldBeCreatedWhenValidPasswordIsGivenTest()
        {
            // Arrange
            var testingPassword = "Test@123";

            // Act
            var validPassword = new Password(testingPassword);

            // Assert
            Regex.IsMatch(testingPassword, PasswordPattern).Should().BeTrue();
            validPassword.Should().NotBeNull();
            validPassword.Value.Should().Be(testingPassword);
        }
    }
}
