using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TicketRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _dbContext.Tickets
            .Include(t => t.Event)
            .Include(t => t.User)
            .SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _dbContext.Tickets
            .Include(t => t.Event)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsByUserIdAsync(int userId)
    {
        return await _dbContext.Tickets
            .Include(t => t.Event)
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        _dbContext.Tickets.Add(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        _dbContext.Tickets.Update(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(int id)
    {
        var ticketToDelete = await _dbContext.Tickets.FindAsync(id);
        if (ticketToDelete != null)
        {
            _dbContext.Tickets.Remove(ticketToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}