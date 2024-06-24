using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<IEnumerable<Event>> GetAllEventsAsync() => await eventRepository.GetAllEventsAsync();

    public async Task<Event> GetEventByIdAsync(int id)
    {
        var ev = await eventRepository.GetEventByIdAsync(id);
        if (ev == null) throw new KeyNotFoundException($"Event with id {id} not exist.");
        return ev;
    }

    public async Task CreateEventAsync(Event ev) => await eventRepository.AddEventAsync(ev);

    public async Task UpdateEventAsync(Event ev)
    {
        var eventToUpdate = await eventRepository.GetEventByIdAsync(ev.Id);
        if (eventToUpdate == null) throw new KeyNotFoundException($"Event with id {ev.Id} not exist.");

        eventToUpdate.Description = ev.Description;
        eventToUpdate.Date = ev.Date;
        eventToUpdate.Organizer = ev.Organizer;
        eventToUpdate.Name = ev.Name;
        
        await eventRepository.UpdateEventAsync(eventToUpdate);
    }

    public async Task DeleteEventAsync(int id)
    {
        var ev = await eventRepository.GetEventByIdAsync(id);
        if (ev == null) throw new KeyNotFoundException($"Event with id {id} not exist.");
        await eventRepository.DeleteEventAsync(id);
    }
}