using HealthTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTrack.API.Data;

public class HealthTrackDbContext : DbContext
{
    public HealthTrackDbContext(DbContextOptions<HealthTrackDbContext> options) : base(options)
    {
    }

    public DbSet<Glucose> GlucoseMeasurements => Set<Glucose>();
    public DbSet<BloodPressure> BloodPressureMeasurements => Set<BloodPressure>();
}
