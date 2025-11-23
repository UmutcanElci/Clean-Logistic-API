using Logistics.Web.Components;
using Microsoft.EntityFrameworkCore;
using Logistics.Infrastructure;
using Logistics.Application.Interfaces;
using Logistics.Infrastructure.Repositories;
using Logistics.Application.Services;
using Logistics.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var DbPassword = builder.Configuration["Database:Password"];
var connectionTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService>();


var correctedTemplate = connectionTemplate.Replace("_DB_PASSWORD_", DbPassword);
builder.Services.AddDbContext<LogisticsDbContext>(options =>
{
    options.UseNpgsql(correctedTemplate);
});

var app = builder.Build();

app.MapPost("/orders", async (CreateOrderDto newOrder, OrderService service) =>
{
    var createdOrder = await service.CreateOrderAsync(newOrder);
    return Results.Created($"/orders/{createdOrder.Id}", createdOrder);
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
