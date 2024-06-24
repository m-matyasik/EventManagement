
namespace Application.DTOs;

public class EventDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public int OrganizerId { get; set; }
    public UserDto Organizer { get; set; }
}