using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface ITicketService
{
    Task CreateTicketAsync(Ticket ticket);
    Task<Ticket> GetTicketByIdAsync(int id);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(int id);
}