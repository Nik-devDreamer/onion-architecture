using Application.Repositories;
using AutoFixture;
using Domain.Entities.Users;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class RoleRepositoryTests
{
    private Fixture _fixture;
    private IRoleRepository _roleRepository;
    private Mock<IRoleRepository> _mockRoleRepository;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _mockRoleRepository = new Mock<IRoleRepository>();
        _roleRepository = _mockRoleRepository.Object;
    }

    [Test]
    public void GetById_ValidId_ReturnsRoleTest()
    {
        // Arrange
        var expectedRole = new Role("TestRole");
        _mockRoleRepository.Setup(repo => repo.GetById(expectedRole.Id)).Returns(expectedRole);

        // Act
        var result = _roleRepository.GetById(expectedRole.Id);

        // Assert
        Assert.That(result, Is.EqualTo(expectedRole));
    }

    [Test]
    public void Add_ValidRole_AddsRoleTest()
    {
        // Arrange
        var role = new Role("TestRole");

        // Act
        _roleRepository.Add(role);

        // Assert
        _mockRoleRepository.Verify(repo => repo.Add(role), Times.Once);
    }
}