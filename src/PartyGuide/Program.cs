using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PartyGuide.Configuration;
using PartyGuide.Persistence;
using PartyGuide.Persistence.Repositories;
using PartyGuide.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddSwaggerGen();

// Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Configuration
var config = builder.Configuration.Get<Config>();
builder.Services.AddSingleton(_ => config);

// Services
builder.Services.AddTransient<IGameService, GameService>();

// Persistence
builder.Services.AddDbContext<ApplicationDbContext>();

using var dbContext = new ApplicationDbContext(config);
dbContext.Database.Migrate();

builder.Services.AddScoped<IGameRepository, GameRepository>();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();

public partial class Program { }
