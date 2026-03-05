using HealthTrack.API.Data;
using HealthTrack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BloodPressureController : ControllerBase
{
    private readonly HealthTrackDbContext _dbContext;

    public BloodPressureController(HealthTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BloodPressure>>> GetAll()
    {
        var measurements = await _dbContext.BloodPressureMeasurements
            .AsNoTracking()
            .OrderByDescending(x => x.DateTime)
            .ToListAsync();

        return Ok(measurements);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BloodPressure>> GetById(int id)
    {
        var measurement = await _dbContext.BloodPressureMeasurements
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (measurement is null)
        {
            return NotFound();
        }

        return Ok(measurement);
    }

    [HttpPost]
    public async Task<ActionResult<BloodPressure>> Create([FromBody] BloodPressure measurement)
    {
        if (measurement.DateTime == default)
        {
            measurement.DateTime = DateTime.UtcNow;
        }

        _dbContext.BloodPressureMeasurements.Add(measurement);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = measurement.Id }, measurement);
    }
}
