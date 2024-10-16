using AccountService.Client.Clients;
using AccountService.Client.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add client service
builder.Services.AddHttpClient<ITransactionClient, TransactionHttpClient>(client =>
{
    var transactionServiceBaseUrl = config["API:Transaction:BaseUrl"];

    if (transactionServiceBaseUrl != null)
    {
        client.BaseAddress = new Uri(transactionServiceBaseUrl);
    }
});

// Add services to the container.

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

app.Run();
