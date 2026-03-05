using HealthTrack.API.Data;
using HealthTrack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GlucoseController : ControllerBase
{
    private readonly HealthTrackDbContext _dbContext;

    public GlucoseController(HealthTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Glucose>>> GetAll()
    {
        var measurements = await _dbContext.GlucoseMeasurements
            .AsNoTracking()
            .OrderByDescending(x => x.DateTime)
            .ToListAsync();

        return Ok(measurements);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Glucose>> GetById(int id)
    {
        var measurement = await _dbContext.GlucoseMeasurements
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (measurement is null)
        {
            return NotFound();
        }

        return Ok(measurement);
    }

    [HttpPost]
    public async Task<ActionResult<Glucose>> Create([FromBody] Glucose measurement)
    {
        if (measurement.DateTime == default)
        {
            measurement.DateTime = DateTime.UtcNow;
        }

        _dbContext.GlucoseMeasurements.Add(measurement);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = measurement.Id }, measurement);
    }
}
