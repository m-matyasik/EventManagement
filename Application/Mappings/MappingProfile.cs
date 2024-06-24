using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>();
        CreateMap<Ticket, TicketDto>();
        CreateMap<Venue, VenueDto>();

    }
}