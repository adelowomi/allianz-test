using Allianz;
using Allianz.Models.Seeders;
using Allianz.Repositories.Interfaces;
using Allianz.Services;
using Allianz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Allianz.Repositories;
using AutoMapper;
using LocalPay.Extensions;
using LocalPay.Flutterwave.Models;
using Allianz.Models.UtilitiModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var AppSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(AppSettingsSection);
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUtilitiesService, UtilitiesServices>();
var AppSettings = AppSettingsSection.Get<AppSettings>();

// add in memory database
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AllianzDb"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// add local pay to enable simple payment via flutterwave
builder.Services.AddFlutterwave(new FlutterwaveInitializationPayload()
{
    SecretKey = AppSettings.FlutterwaveSecretKey,
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // use context
    new Seeder(context).Seed();
}

app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
