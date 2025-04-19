using FinalTouch.Api.Middleware;
using FinalTouch.Api.SignalR;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using FinalTouch.InfraStructure.Data;
using FinalTouch.InfraStructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("https://localhost:4200") // Your Angular dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
	var connString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Connection string 'Redis' not found.");
	var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp"); // Use the CORS policy

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
app.MapHub<NotificationHub>("/hub/notifications");

try{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedDataAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
