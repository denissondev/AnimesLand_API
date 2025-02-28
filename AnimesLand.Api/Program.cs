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
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnimeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(CreateAnimeCommandHandler)));
#pragma warning restore CS8604 // Possible null reference argument.

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Configurar Serilog
LoggingConfiguration.ConfigureLogging();
builder.Host.UseSerilog();

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

var app = builder.Build();

var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    $"Web APi - {description.GroupName.ToUpper()}");
            }
        });
    }

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
