using Application.Services;
using FluentAssertions;
using Moq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

public class VenueRepositoryTests
{
    private readonly Mock<IVenueRepository> _mockRepo;
    private readonly VenueService _venueService;

    public VenueRepositoryTests()
    {
        _mockRepo = new Mock<IVenueRepository>();
        _venueService = new VenueService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllVenues()
    {
        // Arrange
        var venues = new List<Venue>
        {
            new Venue { Id = 1, Name = "Venue 1" },
            new Venue { Id = 2, Name = "Venue 2" }
        };
        _mockRepo.Setup(repo => repo.GetAllVenuesAsync()).ReturnsAsync(venues);

        // Act
        var result = await _venueService.GetAllVenuesAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(venues);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectVenue()
    {
        // Arrange
        var venue1 = new Venue { Id = 1, Name = "Venue 1" };
        _mockRepo.Setup(repo => repo.GetVenueByIdAsync(1)).ReturnsAsync(venue1);

        // Act
        var result = await _venueService.GetVenueByIdAsync(1);

        // Assert
        result.Should().BeEquivalentTo(venue1);
    }

    [Fact]
    public async Task AddAsync_ShouldAddVenue()
    {
        // Arrange
        var newVenue = new Venue { Id = 3, Name = "Venue 3" };
        _mockRepo.Setup(repo => repo.AddVenueAsync(newVenue)).Returns(Task.CompletedTask);

        // Act
        await _venueService.CreateVenueAsync(newVenue);

        // Assert
        _mockRepo.Verify(repo => repo.AddVenueAsync(newVenue), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateVenue()
    {
        // Arrange
        var venue = new Venue() { Id = 1, Name = "Venue 1" };
        _mockRepo.Setup(repo => repo.GetVenueByIdAsync(1)).ReturnsAsync(venue);

        // Act
        venue.Name = "Updated Venue 1";
        await _venueService.UpdateVenueAsync(venue);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateVenueAsync(venue), Times.Once);
    }
    

    [Fact]
    public async Task DeleteAsync_ShouldDeleteVenue()
    {
        // Arrange
        var venue = new Venue() { Id = 1, Name = "Venue 1" };
        _mockRepo.Setup(repo => repo.GetVenueByIdAsync(1)).ReturnsAsync(venue);

        // Act
        await _venueService.DeleteVenueAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteVenueAsync(1), Times.Once);
    }
}
