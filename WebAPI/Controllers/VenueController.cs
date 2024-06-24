using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VenueController(IVenueRepository _venueRepository) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Venue>> GetVenue(int id)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(id);
        if (venue == null)
        {
            return NotFound();
        }
        return venue;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venue>>> GetVenues() => Ok(await _venueRepository.GetAllVenuesAsync());

    [HttpPost]
    public async Task<ActionResult<Venue>> CreateVenue(Venue venue)
    {
        await _venueRepository.AddVenueAsync(venue);
        return CreatedAtAction(nameof(GetVenue), new { id = venue.Id }, venue);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateVenue(int id, Venue venue)
    {
        if (id != venue.Id)
        {
            return BadRequest();
        }
        await _venueRepository.UpdateVenueAsync(venue);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVenue(int id)
    {
        await _venueRepository.DeleteVenueAsync(id);
        return NoContent();
    }
}