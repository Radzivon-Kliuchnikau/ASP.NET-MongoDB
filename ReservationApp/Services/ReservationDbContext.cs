using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;

namespace ReservationApp.Services;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Reservation> Reservations { get; init; }

    public DbSet<Restaurant> Restaurants { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>();
        modelBuilder.Entity<Reservation>();
    }
}