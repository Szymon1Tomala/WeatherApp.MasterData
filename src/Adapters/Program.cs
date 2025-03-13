using Adapters.Inbound.Rest;
using Adapters.Outbound.RabbitMQ;
using Domain.Interfaces.Users;
using Domain.Persistence;
using Domain.Persistence.Entities;
using Domain.Services.RabbitMQ;
using Domain.Services.Users;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<DatabaseContext>();
builder.Services.AddTransient<IUserCreationService, UserCreationService>();
builder.Services.AddTransient<IChangeUserPasswordService, ChangeUserPasswordService>();
builder.Services.AddTransient<IChangeUserEmailService, ChangeUserEmailService>();
builder.Services.AddTransient<IEditUserService, EditUserService>();
builder.Services.AddScoped<EventDeliveryService>();
builder.Services.AddHangfireServer();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.Development.json")
        .Build();

    options.UseSqlServer(configuration.GetConnectionString("Connection"));
});
builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("Connection"))
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings();
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = builder.Configuration.GetSection("HangfireSettings:UserName").Value,
            Pass = builder.Configuration.GetSection("HangfireSettings:Password").Value
        }
    ]
});

RecurringJob.AddOrUpdate<EventDeliveryBackgroundJob>("publishing-events", 
    service => service.PublishEvents(), "*/5 * * * *");

app.UseHttpsRedirection();
app.ConfigureUserPreferencesEndpoints();

app.Run();

public partial class Program;