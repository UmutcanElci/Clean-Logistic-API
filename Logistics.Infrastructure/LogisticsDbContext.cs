using Microsoft.EntityFrameworkCore;
public class LogisticsDbContext : DbContext
{
    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) { }

    public DbSet<Hub> Hub { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Vehicle>().OwnsOne(o => o.CurrentLocation, location_builder =>
        {
            location_builder.OwnsOne(location => location.GpsCoordinates);
        });
        modelBuilder.Entity<Warehouse>().OwnsOne(o => o.Address, coordinate_builder =>
        {
            coordinate_builder.OwnsOne(o => o.GpsCoordinates);
        });
        modelBuilder.Entity<Order>().OwnsMany(o => o.OrderItems);
        modelBuilder.Entity<Order>().OwnsOne(o => o.DestinationLocation, location_builder =>
        {
            location_builder.OwnsOne(o => o.GpsCoordinates);
        });
        modelBuilder.Entity<Order>().OwnsOne(o => o.PickUpLocation, location_builder =>
        {
            location_builder.OwnsOne(o => o.GpsCoordinates);
        });
        modelBuilder.Entity<Route>().OwnsMany(o => o.Stops, location_builder =>
        {
            location_builder.OwnsOne(o => o.GpsCoordinates);
        });

    }
}
