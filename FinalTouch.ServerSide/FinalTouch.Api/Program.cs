using FinalTouch.Api.Middleware;
using FinalTouch.Api.SignalR;
using FinalTouch.Core.Entities;
<<<<<<< HEAD
using Microsoft.AspNetCore.Identity;
=======
using FinalTouch.Core.Interfaces;
using FinalTouch.InfraStructure.Data;
using FinalTouch.InfraStructure.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
>>>>>>> origin/main
using StackExchange.Redis;
using FinalTouch.InfraStructure.Data;
using FinalTouch.Application;

var builder = WebApplication.CreateBuilder(args);

// App Services (Infrastructure Layer)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Redis separately
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var connString = builder.Configuration.GetConnectionString("Redis")
        ?? throw new Exception("Redis connection string not found.");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});

// Identity
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Controllers, Swagger, SignalR, etc.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
<<<<<<< HEAD
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddApplicationServices();
=======
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
	var connString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Connection string 'Redis' not found.");
	var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IResponseCacheService, ResponseCacheService>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddSignalR();


var app = builder.Build();
>>>>>>> origin/main

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
app.MapHub<NotificationHub>("/hub/notifications");

<<<<<<< HEAD
=======
try{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedDataAsync(context, userManager);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

>>>>>>> origin/main
app.Run();
