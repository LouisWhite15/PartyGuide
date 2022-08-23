using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PartyGuide.Configuration;
using PartyGuide.Persistence;
using PartyGuide.Persistence.Repositories;
using PartyGuide.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite($"Data Source={config.ConnectionStrings.Sqlite}");
});

builder.Services.AddScoped<IGameRepository, GameRepository>();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Migrate database
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    
    try
    {
        Log.Logger.Information("Migrating database");

        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Log.Logger.Error(ex, "Migration failed");
    }

    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();

public partial class Program { }
