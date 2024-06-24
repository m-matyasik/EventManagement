namespace Application.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int EventId { get; set; }
    public EventDto Event { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; }
}