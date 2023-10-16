using System;
using NUnit.Framework;
using AutoFixture;
using FluentAssertions;
using Onion_architecture.Domain.BaseObjectsNamespace;
using Onion_architecture.Domain.Entities.Users;

namespace Onion_architecture.Tests.Tests.Domain
{
    [TestFixture]
    class UserTests
    {
        private Fixture _fixture;

        public UserTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Constructor_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            var user = new User(id, name, email, roleId, password);

            // Assert
            user.Id.Should().Be(id);
            user.Name.Should().Be(name);
            user.Email.Value.Should().Be(email.Value);
            user.RoleId.Should().Be(roleId);
            user.Password.Value.Should().Be(password.Value);
        }

        [Test]
        public void User_Id_CorrectlySet()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            var user = new User(id, name, email, roleId, password);

            // Assert
            user.Id.Should().Be(id);
        }

        [Test]
        public void User_Name_CorrectlySet()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            var user = new User(id, name, email, roleId, password);

            // Assert
            user.Name.Should().Be(name);
        }

        [Test]
        public void User_Email_CorrectlySet()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            var user = new User(id, name, email, roleId, password);

            // Assert
            user.Email.Should().Be(email);
        }

        [Test]
        public void User_RoleId_CorrectlySet()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            var user = new User(id, name, email, roleId, password);

            // Assert
            user.RoleId.Should().Be(roleId);
        }

        [Test]
        public void User_Password_CorrectlySet()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            var user = new User(id, name, email, roleId, password);

            // Assert
            user.Password.Should().Be(password);
        }

        [Test]
        public void User_Constructor_NullNameThrowsArgumentNullException()
        {
            // Arrange
            string name = null;
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            Action createAction = () => new User(_fixture.Create<Guid>(), name, email, roleId, password);

            // Assert
            createAction.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("name");
        }

        [Test]
        public void User_Constructor_NullEmailThrowsArgumentNullException()
        {
            // Arrange
            string name = _fixture.Create<string>();
            Email email = null;
            var roleId = _fixture.Create<Guid>();
            var password = new Password("Test@123");

            // Act
            Action createAction = () => new User(_fixture.Create<Guid>(), name, email, roleId, password);

            // Assert
            createAction.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("email");
        }

        [Test]
        public void User_Constructor_NullPasswordThrowsArgumentNullException()
        {
            // Arrange
            var roleId = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            Password password = null;

            // Act
            Action createAction = () => new User(_fixture.Create<Guid>(), name, email, roleId, password);

            // Assert
            createAction.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("password");
        }

        [Test]
        public void User_Create_CreatesValidUser()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var role = new Role(_fixture.Create<string>());
            var email = new Email(_fixture.Create<string>() + "@gmail.com");
            var password = new Password("Test@123");

            // Act
            var user = User.Create(name, email, role, password);

            // Assert
            user.Should().NotBeNull();
            user.Name.Should().Be(name);
            user.Email.Should().Be(email);
            user.RoleId.Should().Be(role.Id);
            user.Password.Should().Be(password);
        }
    }
}
