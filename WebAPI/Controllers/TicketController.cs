using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TicketController(ITicketRepository _ticketRepository) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return ticket;
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByUserId(int userId) =>
        Ok(await _ticketRepository.GetAllTicketsByUserIdAsync(userId));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets() =>
        Ok(await _ticketRepository.GetAllTicketsAsync());

    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
    {
        await _ticketRepository.AddTicketAsync(ticket);
        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
    {
        if (id != ticket.Id)
        {
            return BadRequest();
        }
        await _ticketRepository.UpdateTicketAsync(ticket);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        await _ticketRepository.DeleteTicketAsync(id);
        return NoContent();
    }
}