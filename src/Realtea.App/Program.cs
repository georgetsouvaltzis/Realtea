using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Realtea.App.Authorization;
using Realtea.App.HttpContextWrapper;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Interfaces.Services;
using Realtea.Core.Repositories;
using Realtea.Core.Services;
using Realtea.Infrastructure;
using Realtea.Infrastructure.Identity;
using Realtea.Infrastructure.Identity.Authentication;
using Realtea.Infrastructure.Repositories;
using Realtea.Infrastructure.Seeder;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<RealTeaDbContext>();

builder.Services.AddAuthentication(auth =>
{

    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "iss",
            ValidAudience = "aud",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey123")),
        };
    });

builder.Services.AddAuthorization(options =>
{
    // https://stackoverflow.com/questions/45807822/asp-net-core-2-0-jwt-validation-fails-with-authorization-failed-for-user-null
    var builder = new AuthorizationPolicyBuilder();

    builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

    builder.RequireAuthenticatedUser();

    options.DefaultPolicy = builder.Build();


    options.AddPolicy("IsEligibleForAdvertisementDelete", policy =>
    {
        policy.AddRequirements(new IsEligibleForAdvertisementDeleteRequirement());
    });
});

builder.Services.AddScoped<IHttpContextAccessorWrapper, HttpContextAccessorWrapper>();

builder.Services.AddScoped<IAuthorizationHandler, IsEligibleForAdvertisementDeleteAuthHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsEligibleForAdvertisementUpdateAuthHandler>();
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
//builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<RealTeaDbContext>(options => options.UseInMemoryDatabase("realteaDb"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

await DatabaseInitializer.InitializeAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
