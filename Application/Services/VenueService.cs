using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class VenueService(IVenueRepository venueRepository) : IVenueService
{
    public async Task CreateVenueAsync(Venue venue) => await venueRepository.AddVenueAsync(venue);

    public async Task<Venue> GetVenueByIdAsync(int id)
    {
        var venue = await venueRepository.GetVenueByIdAsync(id);
        if (venue == null) throw new KeyNotFoundException($"Venue with id {id} not exist.");
        return venue;
    }

    public async Task<IEnumerable<Venue>> GetAllVenuesAsync() => await venueRepository.GetAllVenuesAsync();

    public async Task UpdateVenueAsync(Venue venue)
    {
        var toUpdate = await venueRepository.GetVenueByIdAsync(venue.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"Venue with id {venue.Id} not exist.");

        toUpdate.Events = venue.Events;
        toUpdate.Name = venue.Name;
        toUpdate.Location = venue.Location;
        
        await venueRepository.UpdateVenueAsync(toUpdate);
    }

    public async Task DeleteVenueAsync(int id) => await venueRepository.DeleteVenueAsync(id);
}