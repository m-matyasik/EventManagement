using Application.Services;
using FluentAssertions;
using Moq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

public class EventRepositoryTests
{
    private readonly Mock<IEventRepository> _mockRepo;
    private readonly EventService _eventService;

    public EventRepositoryTests()
    {
        _mockRepo = new Mock<IEventRepository>();
        _eventService = new EventService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEvents()
    {
        // Arrange
        var events = new List<Event>
        {
            new Event { Id = 1, Name = "Event 1", Date = DateTime.Now.AddDays(1) },
            new Event { Id = 2, Name = "Event 2", Date = DateTime.Now.AddDays(2) }
        };
        _mockRepo.Setup(repo => repo.GetAllEventsAsync()).ReturnsAsync(events);

        // Act
        var result = await _eventService.GetAllEventsAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(events);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectEvent()
    {
        // Arrange
        var event1 = new Event { Id = 1, Name = "Event 1", Date = DateTime.Now.AddDays(1) };
        _mockRepo.Setup(repo => repo.GetEventByIdAsync(1)).ReturnsAsync(event1);

        // Act
        var result = await _eventService.GetEventByIdAsync(1);

        // Assert
        result.Should().BeEquivalentTo(event1);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEvent()
    {
        // Arrange
        var newEvent = new Event { Id = 3, Name = "Event 3", Date = DateTime.Now.AddDays(3) };
        _mockRepo.Setup(repo => repo.AddEventAsync(newEvent)).Returns(Task.CompletedTask);

        // Act
        await _eventService.CreateEventAsync(newEvent);

        // Assert
        _mockRepo.Verify(repo => repo.AddEventAsync(newEvent), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEvent()
    {
        // Arrange
        var eventItem = new Event() { Id = 1, Name = "Cool Event" };
        _mockRepo.Setup(repo => repo.GetEventByIdAsync(1)).ReturnsAsync(eventItem);

        // Act
        eventItem.Name = "Mega cool event";
        await _eventService.UpdateEventAsync(eventItem);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateEventAsync(eventItem), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEvent_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetEventByIdAsync(It.IsAny<int>())).ReturnsAsync((Event)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _eventService.DeleteEventAsync(1));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEvent()
    {
        // Arrange
        var eventItem = new Event() { Id = 1, Name = "Cool Event" };
        _mockRepo.Setup(repo => repo.GetEventByIdAsync(1)).ReturnsAsync(eventItem);

        // Act
        await _eventService.DeleteEventAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteEventAsync(1), Times.Once);
    }
}
