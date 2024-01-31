using Microsoft.EntityFrameworkCore;
using RedisApp.DataAccess.Interface;
using RedisApp.DataAccess.Repository;
using RedisApp.Database.Context;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Db context changes
builder.Services.AddDbContext<TestingContext>(opt => 
opt.UseNpgsql("Host=localhost; Port=5432; Database=Shopping; User ID=postgres; Password=1234;"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//redis dependency
builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    return ConnectionMultiplexer.Connect("localhost:6379");
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderRepo, OrderRepo>();

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
