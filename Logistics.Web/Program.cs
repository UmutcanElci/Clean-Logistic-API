using Logistics.Web.Components;
using Microsoft.EntityFrameworkCore;
using Logistics.Infrastructure;
using Logistics.Application.Interfaces;
using Logistics.Infrastructure.Repositories;
using Logistics.Application.Services;
using Logistics.Application.DTOs;
using Logistics.Domain;
using Logistics.Domain.common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var DbPassword = builder.Configuration["Database:Password"];
var connectionTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IHubRepository, HubRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<HubService>();
builder.Services.AddHttpClient<IGeocodingService, NominatimGeocodingService>();
builder.Services.AddScoped<RouteService>();

builder.Services.AddControllers();

var correctedTemplate = connectionTemplate!.Replace("_DB_PASSWORD_", DbPassword);
builder.Services.AddDbContext<LogisticsDbContext>(options =>
{
    options.UseNpgsql(correctedTemplate);
});

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    var services = scoped.ServiceProvider;
    var context = services.GetRequiredService<LogisticsDbContext>();

    if (!context.Hub.Any())
    {
        var initialHub = new Hub
        {
            Id = Guid.NewGuid(),
        };

        context.Hub.Add(initialHub);
        context.SaveChanges();
    }
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
