using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EventController(IEventRepository eventRepository) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var eventItem = await eventRepository.GetEventByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        return eventItem;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents() => Ok(await eventRepository.GetAllEventsAsync());


    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(Event eventItem)
    {
        await eventRepository.AddEventAsync(eventItem);
        return CreatedAtAction(nameof(GetEvent), new { id = eventItem.Id }, eventItem);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, Event eventItem)
    {
        if (id != eventItem.Id)
        {
            return BadRequest();
        }
        await eventRepository.UpdateEventAsync(eventItem);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        await eventRepository.DeleteEventAsync(id);
        return NoContent();
    }
}