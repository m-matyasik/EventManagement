namespace Application.DTOs;

public class VenueDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public ICollection<EventDto> Events { get; set; } = [];
}