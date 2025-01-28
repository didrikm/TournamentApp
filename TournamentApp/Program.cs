using Microsoft.EntityFrameworkCore;
using TournamentApi.Extensions;
using TournamentData.Data;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<TournamentApiContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'TournamentApiContext' not found.")));
var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DATABASE_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Connection string environment variable 'ASPNETCORE_DATABASE_CONNECTION_STRING' not found.");


// Add services to the container.

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.ConfigureServiceLayerServices();
builder.Services.ConfigureRepositories();
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseSwagger();
//app.UseSwaggerUI();
await app.SeedDataAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
