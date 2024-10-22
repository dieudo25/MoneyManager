using AccountService.Client.Clients;
using AccountService.Client.Interfaces;
using AccountService.Database.Context;
using AccountService.Database.Repositories;
using AccountService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

//// Add DBContext
builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Add Repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

// Add client service
builder.Services.AddHttpClient<ITransactionClient, TransactionHttpClient>(client =>
{
    var transactionServiceBaseUrl = config["API:Transaction:BaseUrl"];

    if (transactionServiceBaseUrl != null)
    {
        client.BaseAddress = new Uri(transactionServiceBaseUrl);
    }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting up account service");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Account service run has failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
