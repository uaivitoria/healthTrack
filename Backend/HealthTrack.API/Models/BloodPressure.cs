namespace HealthTrack.API.Models;

public class BloodPressure
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int SystolicPressure { get; set; }
    public int DiastolicPressure { get; set; }
    public string? Note { get; set; }
}
