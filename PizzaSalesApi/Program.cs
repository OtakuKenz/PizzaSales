using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models.Entities;
using PizzaSalesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PizzaSalesContext>(options =>
    options.UseSqlite("Data Source=pizzasales.db"));

// Add CORS policy
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost5001",
      policy => policy.WithOrigins("http://localhost:5001")
                      .AllowAnyHeader()
                      .AllowAnyMethod());
});

// Dependency injection for services
builder.Services.AddScoped<IPizzaTypeService, PizzaTypeService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();

// Configure to run on specific port
// This is for compatibility with the angular frontend
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5264); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowLocalhost5001");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
