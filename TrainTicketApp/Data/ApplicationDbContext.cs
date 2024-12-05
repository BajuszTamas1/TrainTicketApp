// TrainTicketApp/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using TrainTicketApp.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Train> Trains { get; set; }
    public DbSet<Order> Orders { get; set; }
// TrainTicketApp/Data/ApplicationDbContext.cs
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
}