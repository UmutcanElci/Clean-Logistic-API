using Microsoft.EntityFrameworkCore;
public class LogisticsDbContext : DbContext
{
    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) { }

    public DbSet<Hub> Hub { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

}
