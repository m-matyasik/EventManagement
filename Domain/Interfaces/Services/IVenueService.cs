using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IVenueService
{
    Task CreateVenueAsync(Venue venue);
    Task<Venue> GetVenueByIdAsync(int id);
    Task<IEnumerable<Venue>> GetAllVenuesAsync();
    Task UpdateVenueAsync(Venue venue);
    Task DeleteVenueAsync(int id);
}