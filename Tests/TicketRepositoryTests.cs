using Application.Services;
using FluentAssertions;
using Moq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

public class TicketRepositoryTests
{
    private readonly Mock<ITicketRepository> _mockRepo;
    private readonly TicketService _ticketService;

    public TicketRepositoryTests()
    {
        _mockRepo = new Mock<ITicketRepository>();
        _ticketService = new TicketService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTickets()
    {
        // Arrange
        var tickets = new List<Ticket>
        {
            new Ticket { Id = 1, EventId = 1 },
            new Ticket { Id = 2, EventId = 1 }
        };
        _mockRepo.Setup(repo => repo.GetAllTicketsAsync()).ReturnsAsync(tickets);

        // Act
        var result = await _ticketService.GetAllTicketsAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(tickets);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectTicket()
    {
        // Arrange
        var ticket1 = new Ticket { Id = 1, EventId = 1 };
        _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(ticket1);

        // Act
        var result = await _ticketService.GetTicketByIdAsync(1);

        // Assert
        result.Should().BeEquivalentTo(ticket1);
    }

    [Fact]
    public async Task AddAsync_ShouldAddTicket()
    {
        // Arrange
        var newTicket = new Ticket { Id = 3, EventId = 2 };
        _mockRepo.Setup(repo => repo.AddTicketAsync(newTicket)).Returns(Task.CompletedTask);

        // Act
        await _ticketService.CreateTicketAsync(newTicket);

        // Assert
        _mockRepo.Verify(repo => repo.AddTicketAsync(newTicket), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTicket()
    {
        // Arrange
        var ticket = new Ticket() { Id = 1, EventId = 1 };
        _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(ticket);

        // Act
        ticket.EventId = 2; // Update EventId for example
        await _ticketService.UpdateTicketAsync(ticket);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateTicketAsync(ticket), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTicket_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetTicketByIdAsync(It.IsAny<int>())).ReturnsAsync((Ticket)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _ticketService.DeleteTicketAsync(1));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTicket()
    {
        // Arrange
        var ticket = new Ticket() { Id = 1, EventId = 1 };
        _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(ticket);

        // Act
        await _ticketService.DeleteTicketAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteTicketAsync(1), Times.Once);
    }
}
