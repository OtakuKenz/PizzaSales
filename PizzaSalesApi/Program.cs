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

// Dependency injection for services
builder.Services.AddScoped<IPizzaTypeService, PizzaTypeService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
