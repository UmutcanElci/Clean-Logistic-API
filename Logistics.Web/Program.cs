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
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<WarehouseService>();

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


app.MapPost("/warehouses", async (CreateWarehouseDto newWarehouseDto, WarehouseService service) =>
{
    var createdWarehouse = await service.CreateWarehouseAsync(newWarehouseDto);
    return Results.Created($"/warehouses/{createdWarehouse.Id}", createdWarehouse);
});

app.MapGet("/warehouses", async (WarehouseService service) =>
{
    var warehouse = await service.GetAllWarehousesAsync();
    return Results.Ok(warehouse);
});

app.MapGet("/warehouses/{id:guid}", async (Guid id, WarehouseService service) =>
{
    var warehouse = await service.GetWarehouseByIdAsync(id);
    return warehouse != null ? Results.Ok(warehouse) : Results.NotFound();
});

app.MapPut("/warehouses/{id:guid}", async (Guid id, UpdateWarehouseDto updateWarehouseDto, WarehouseService service) =>
{
    if (id != updateWarehouseDto.Id)
    {
        return Results.BadRequest("Route ID and warehouse ID in body must match.");
    }
    try
    {
        await service.UpdateWarehouseAsync(updateWarehouseDto);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(ex.Message);
    }
});

app.MapDelete("/warehouses/{id:guid}", async (Guid id, WarehouseService service) =>
{
    await service.DeleteWarehouseAsync(id);
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
