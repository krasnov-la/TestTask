using Microsoft.EntityFrameworkCore;
using TestTasks.Domain;

namespace TestTasks.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors {  get; set; }
    public DbSet<Patient> Patients {  get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Specialization> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}