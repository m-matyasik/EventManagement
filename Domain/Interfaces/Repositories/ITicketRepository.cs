using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<Ticket> GetTicketByIdAsync(int id);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetAllTicketsByUserIdAsync(int userId);
    Task AddTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(int id);
}