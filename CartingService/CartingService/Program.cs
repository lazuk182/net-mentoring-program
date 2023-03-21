using CartingService.BLL.Entities;
using CartingService.BLL.Services;
using CartingService.DAL;
using CartingService.DAL.Database;
using MaikeBing.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContextPool<CartingContext>(options =>
//{
//    options.UseInMemoryDatabase(databaseName: "CartingDb");
//});
//builder.Services.AddScoped<ICartData, CartData>();

var Database = new LiteDB.LiteDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<ICartData, CartLiteDb>(provider => new CartLiteDb(Database));

builder.Services.AddScoped<ICartService, Cart>();
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
