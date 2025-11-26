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
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<VehicleService>();

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


app.MapPost("/vehicles", async (CreateVehicleDto newVehicleDto, VehicleService service) =>
{
    var createdVehicle = await service.CreateVehicleAsync(newVehicleDto);
    return Results.Created($"/vehicles/{createdVehicle.Id}", createdVehicle);
});

app.MapGet("/vehicles", async (VehicleService service) =>
{
    var vehicle = await service.GetAllVehiclesAsync();
    return Results.Ok(vehicle);
});

app.MapGet("/vehicles/{id:guid}", async (Guid id, VehicleService service) =>
{
    var vehicle = await service.GetVehicleByIdAsync(id);
    return vehicle != null ? Results.Ok(vehicle) : Results.NotFound();
});

app.MapPut("/vehicles/{id:guid}", async (Guid id, UpdateVehicleDto updateVehicleDto, VehicleService service) =>
{
    if (id != updateVehicleDto.Id)
    {
        return Results.BadRequest("Route ID and Vehicle ID in body must match.");
    }
    try
    {
        await service.UpdateVehicleAsync(updateVehicleDto);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.MapDelete("/vehicles/{id:guid}", async (Guid id, VehicleService service) =>
{
    await service.DeleteVehicleAsync(id);
    return Results.NoContent();
});



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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
