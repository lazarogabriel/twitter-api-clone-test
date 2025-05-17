using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using twitter.api.data.DbContexts;
using twitter.api.web.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Twitter API",
        Version = "v1"
    });
});

// Adds AutoMapper.
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<TwitterApiDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("TwitterDbContext"),
        b => b.MigrationsAssembly("twitter.api.web"))
    );

var app = builder.Build();

app.UseErrorHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Twitter API V1");
        options.RoutePrefix = string.Empty; // Para que Swagger est� en la ra�z "/"
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();