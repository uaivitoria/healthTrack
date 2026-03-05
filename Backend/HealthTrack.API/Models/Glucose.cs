namespace HealthTrack.API.Models;

public class Glucose
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int Value { get; set; }
    public string? Note { get; set; }
}
