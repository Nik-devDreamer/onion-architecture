using Application.Repositories;
using AutoFixture;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests.Repositories;

[TestFixture]
public class UserRepositoryTests
{
    private Fixture _fixture;
    private IUserRepository _userRepository;
    private Mock<IUserRepository> _mockUserRepository;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        _userRepository = _mockUserRepository.Object;
    }
    
    private User CreateFakeUser()
    {
        var id = _fixture.Create<Guid>();
        var name = _fixture.Create<string>();
        var email = new Email("test@gmail.com");
        var roleId = _fixture.Create<Guid>();
        var password = new Password("Test@123");
        return new User(id, name, email, roleId, password);
    }

    [Test]
    public void TryGetByEmail_ValidEmail_ReturnsUserTest()
    {
        // Arrange
        var email = new Email("test@gmail.com");
        var expectedUser = CreateFakeUser();
        _mockUserRepository.Setup(repo => repo.TryGetByEmail(email)).Returns(expectedUser);

        // Act
        var result = _userRepository.TryGetByEmail(email);

        // Assert
        Assert.That(result, Is.EqualTo(expectedUser));
    }

    [Test]
    public void GetById_ValidId_ReturnsUserTest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedUser = CreateFakeUser();
        _mockUserRepository.Setup(repo => repo.GetById(userId)).Returns(expectedUser);

        // Act
        var result = _userRepository.GetById(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expectedUser));
    }

    [Test]
    public void Add_ValidUser_AddsUserTest()
    {
        // Arrange
        var user = CreateFakeUser();
        _mockUserRepository.Setup(repo => repo.Add(user));

        // Act
        _userRepository.Add(user);

        // Assert
        _mockUserRepository.VerifyAll();
    }

    [Test]
    public void GetAll_ReturnsAllUsersTest()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            CreateFakeUser(),
            CreateFakeUser(),
            CreateFakeUser()
        };
        _mockUserRepository.Setup(repo => repo.GetAll()).Returns(expectedUsers);

        // Act
        var result = _userRepository.GetAll();

        // Assert
        CollectionAssert.AreEqual(expectedUsers, result);
    }
}