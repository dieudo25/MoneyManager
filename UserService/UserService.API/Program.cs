using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Serilog;
using UserService.Database.Context;
using UserService.Database.Repositories;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

//// Add DBContext
builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

//// Add Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

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
    Log.Information("Starting up user service");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "User service run has failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}