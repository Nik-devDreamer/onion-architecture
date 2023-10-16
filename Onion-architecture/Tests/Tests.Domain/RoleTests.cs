using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using Onion_architecture.Domain.Entities.Users;

namespace Onion_architecture.Tests.Tests.Domain
{
    [TestFixture]
    class RoleTests
    {
        private Fixture _fixture;

        public RoleTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Role_CanBeCreatedWithValidName()
        {
            // Arrange
            var roleName = _fixture.Create<string>();

            // Act
            var role = new Role(roleName);

            // Assert
            role.Should().NotBeNull();
            role.Name.Should().Be(roleName);
            role.Id.Should().NotBe(Guid.Empty);
        }

        [Test]
        public void Role_CannotBeCreatedWithNullName()
        {
            // Arrange
            string roleName = null;

            // Act
            Action createRoleAction = () => new Role(roleName);

            // Assert
            createRoleAction.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("name");
        }

        [Test]
        public void Role_HasADifferentId()
        {
            // Arrange
            var roleName = _fixture.Create<string>();

            // Act
            var role1 = new Role(roleName);
            var role2 = new Role(roleName);

            // Assert
            role1.Id.Should().NotBe(role2.Id);
        }
    }
}
