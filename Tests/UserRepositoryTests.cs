using Application.Services;
using FluentAssertions;
using Moq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Enums;
using Xunit;

public class UserRepositoryTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserService _userService;

    public UserRepositoryTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "user1", PasswordHash = "user1", Role = Role.User },
            new User { Id = 2, Username = "user2", PasswordHash = "admin1", Role = Role.Admin }
        };
        _mockRepo.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectUser()
    {
        // Arrange
        var user1 = new User { Id = 1, Username = "user1", PasswordHash = "strongpass", Role = Role.User };
        _mockRepo.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(user1);

        // Act
        var result = await _userService.GetUserByIdAsync(1);

        // Assert
        result.Should().BeEquivalentTo(user1);
    }

    [Fact]
    public async Task AddAsync_ShouldAddUser()
    {
        // Arrange
        var newUser = new User { Id = 3, Username = "user3", PasswordHash = "strongpass", Role = Role.User };
        _mockRepo.Setup(repo => repo.AddUserAsync(newUser)).Returns(Task.CompletedTask);

        // Act
        await _userService.CreateUserAsync(newUser);

        // Assert
        _mockRepo.Verify(repo => repo.AddUserAsync(newUser), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUser()
    {
        // Arrange
        var user = new User() { Id = 1, Username = "user1", PasswordHash = "strongpass", Role = Role.User };
        _mockRepo.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(user);

        // Act
        user.Role = Role.Admin;
        await _userService.UpdateUserAsync(user);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateUserAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteUser_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.DeleteUserAsync(1));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteUser()
    {
        // Arrange
        var user = new User() { Id = 1, Username = "user1", PasswordHash = "strongpass", Role = Role.Admin };
        _mockRepo.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(user);

        // Act
        await _userService.DeleteUserAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteUserAsync(1), Times.Once);
    }
}
