using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Realtea.Core.Repositories;
using Realtea.Core.Services;
using Realtea.Domain.Entities;
using Realtea.Domain.Repositories;
using Realtea.Infrastructure;
using Realtea.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();

builder.Services.AddDbContext<RealTeaDbContext>(options => options.UseInMemoryDatabase("realteaDb"));
builder.Services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<RealTeaDbContext>();

var app = builder.Build();

DatabaseInitializer.Initialize(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
