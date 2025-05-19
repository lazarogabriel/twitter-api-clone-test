using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using twitter.api.web.Extensions;
using twitter.api.application.Models.Security;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Adds Swagger Doc
builder.Services.AddTwitterSwagger();

// Adds AutoMapper.
builder.Services.AddAutoMapper(typeof(Program));

// Adds Database
builder.Services.AddTwitterDatabase(config: configuration);

// Adds Services
builder.Services.AddTwitterServices();

// Adds Authentication and authorization
builder.Services.AddAuthorization();
builder.Services.AddTwitterAuthentication(config: configuration.GetSection("SecuritySettings"));

// Adds Security Settings.
builder.Services.Configure<SecurityServiceConfiguration>(configuration.GetSection("SecuritySettings"));


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
        options.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz "/"
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();