using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly ApplicationDbContext _context;

    public VenueRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Venue?> GetVenueByIdAsync(int id)
    {
        return await _context.Venues
            .Include(v => v.Events)
            .SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<Venue>> GetAllVenuesAsync()
    {
        return await _context.Venues
            .Include(v => v.Events)
            .ToListAsync();
    }

    public async Task AddVenueAsync(Venue venue)
    {
        _context.Venues.Add(venue);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVenueAsync(Venue venue)
    {
        _context.Venues.Update(venue);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVenueAsync(int id)
    {
        var venueToDelete = await _context.Venues.FindAsync(id);
        if (venueToDelete != null)
        {
            _context.Venues.Remove(venueToDelete);
            _context.SaveChangesAsync();
        }
    }
}