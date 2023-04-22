using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Realtea.App.Cache;
using Realtea.App.Filters;
using Realtea.App.HttpContextWrapper;
using Realtea.App.Identity.Authorization.Handlers.Advertisement;
using Realtea.App.Identity.Authorization.Requirements.Advertisement;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Profiles;
using Realtea.Infrastructure;   
using Realtea.Infrastructure.Authentication;
using Realtea.Infrastructure.Identity;
using Realtea.Infrastructure.Repositories;
using Realtea.Infrastructure.Seeder;
using Realtea.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

builder.Services.AddAutoMapper(typeof(Program), typeof(AdvertisementToAdvertisementResultProfile));
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
            ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
            ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SigningKey"])),
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
    options.AddPolicy("IsEligibleForAdvertisementUpdate", policy =>
    {
        policy.AddRequirements(new IsEligibleForAdvertisementUpdateRequirement());
    });
});

builder.Services.AddScoped<IHttpContextAccessorWrapper, HttpContextAccessorWrapper>();

builder.Services.AddScoped<IAuthorizationHandler, IsEligibleForAdvertisementDeleteHandler>();
builder.Services.AddScoped<IAuthorizationHandler,IsEligibleForAdvertisementUpdateAuthHandler>();
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddDbContext<RealTeaDbContext>(options => options.UseInMemoryDatabase("realteaDb"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

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
