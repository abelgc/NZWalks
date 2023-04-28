using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Logger configuration

var logger = new LoggerConfiguration().
    WriteTo.Console()
    .WriteTo.File("Logs/NzWalks.txt", rollingInterval:RollingInterval.Minute)
    .MinimumLevel
    .Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EF DI
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

// Configure the HTTP request PIPELINE. THIS ADDS MIDDLEWARE (ASSEMBLA into app pipeline to handle requests and responses
// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// end middleware
app.Run(); //ab hier se va a program.cs
