using Adapters.Inbound.Rest;
using Domain.Interfaces;
using Domain.Persistence;
using Domain.Persistence.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<DatabaseContext>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.Development.json")
        .Build();

    options.UseSqlServer(configuration.GetConnectionString("Connection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureUserPreferencesEndpoints();

app.Run();

public partial class Program;