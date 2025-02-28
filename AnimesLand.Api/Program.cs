using AnimesLand.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using AnimesLand.Infrastructure.Logging;
using Serilog;
using AnimesLand.Infrastructure.Middleware;
using AnimesLand.Application.Features.Animes.Commands;
using AnimesLand.Infrastructure.Repositories;
using AnimesLand.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnimeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(CreateAnimeCommandHandler)));

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

LoggingConfiguration.ConfigureLogging();
builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
