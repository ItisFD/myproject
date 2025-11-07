using System.Configuration;
using Microsoft.VisualBasic;
using MyBackendApi.Helpers;
using MyBackendApi.Repositories;
using MyBackendApi.Services; // Give access to our DB Helpers

var builder = WebApplication.CreateBuilder(args); // Bootstrap our WebApplication -webserver, conf, logging

// Adding swagger
builder.Services.AddEndpointsApiExplorer(); // Generates metadata for endpoints, needed by swagger to discover routes
builder.Services.AddSwaggerGen(); // builds swagger endpoint for testing API

builder.Services.AddControllers(); // Register MVC style controller -  so [ApiController] can handle requests
builder.Services.AddSingleton<IAccountRepository, AccountRepository>(); // Register DAL as app lifetime service
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IDatabaseHelper>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new("No string");
    return new DatabaseHelper(connectionString);
}); // register DB helper interface with connection string instance


// -- APP Pipeline Build --
var app = builder.Build(); // Builds web host and DI Container
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Generates OpenAPI Json
    app.UseSwaggerUI(); // Serves Web UI for testing endpoints
}

app.UseHttpsRedirection(); // Redirects http to https automatically
app.MapControllers(); // Makes api/ work



app.Run();


