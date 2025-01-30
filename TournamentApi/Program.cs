using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TournamentApi.Extensions;
using TournamentData.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DATABASE_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Connection string environment variable 'ASPNETCORE_DATABASE_CONNECTION_STRING' not found.");

builder.Services.AddDbContext<TournamentApiContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null)));

//builder.Services.AddDbContext<TournamentApiContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
//        maxRetryCount: 5,
//        maxRetryDelay: TimeSpan.FromSeconds(30),
//        errorNumbersToAdd: null)));


// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.ConfigureServiceLayerServices();
builder.Services.ConfigureRepositories();
var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//app.UseSwagger();
//app.UseSwaggerUI();
await app.SeedDataAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "API is running");

app.Run();
