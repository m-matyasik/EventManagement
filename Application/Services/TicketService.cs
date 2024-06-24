using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class TicketService(ITicketRepository ticketRepository) : ITicketService
{
    public async Task CreateTicketAsync(Ticket ticket) => await ticketRepository.AddTicketAsync(ticket);

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        var ticket = await ticketRepository.GetTicketByIdAsync(id);
        if (ticket == null) throw new KeyNotFoundException($"Ticket with ID {id} not found.");
        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync() => await ticketRepository.GetAllTicketsAsync();
    
    public async Task UpdateTicketAsync(Ticket ticket)
    {
        var toUpdate = await ticketRepository.GetTicketByIdAsync(ticket.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"Ticket with id {ticket.Id} not exist.");

        toUpdate.Event = ticket.Event;
        toUpdate.User = ticket.User;
        toUpdate.TicketNumber = ticket.TicketNumber;

        await ticketRepository.UpdateTicketAsync(toUpdate);
    }

    public async Task DeleteTicketAsync(int id)
    {
        var toDelete = await ticketRepository.GetTicketByIdAsync(id);
        if (toDelete == null) throw new KeyNotFoundException($"Ticket with id {id} not exist.");
        await ticketRepository.DeleteTicketAsync(id);
    }
}