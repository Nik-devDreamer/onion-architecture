using AutoFixture;
using Domain.Entities.Users;
using FluentAssertions;
using NUnit.Framework;

namespace DomainTests.Entities.Users
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
        public void Role_CanBeCreatedWithValidNameTest()
        {
            // Arrange
            var roleName = _fixture.Create<string>();

            // Act
            var role = Role.Create(roleName);

            // Assert
            role.Should().NotBeNull();
            role.Name.Should().Be(roleName);
            role.Id.Should().NotBe(Guid.Empty);
        }

        [Test]
        public void Role_CannotBeCreatedWithNullNameTest()
        {
            // Arrange
            string roleName = null;

            // Act
            Action createRoleAction = () => Role.Create(roleName);

            // Assert
            createRoleAction.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("name");
        }

        [Test]
        public void Role_HasADifferentIdTest()
        {
            // Arrange
            var roleName = _fixture.Create<string>();

            // Act
            var role1 = Role.Create(roleName);
            var role2 = Role.Create(roleName);

            // Assert
            role1.Id.Should().NotBe(role2.Id);
        }
    }
}
