using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Data;

public class SpaceDbContext : DbContext
{
    public DbSet<Planet> Planets { get; set; }
    public DbSet<Star> Stars { get; set; }
    public DbSet<Moon> Moons { get; set; }


    public SpaceDbContext(DbContextOptions<SpaceDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}