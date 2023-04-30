using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Realtea.Api.Filters;
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
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Realtea API V1",
        Version = "V1",
        Description = "Sample real-estate like API.",
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    opt.ExampleFilters();
    opt.DocumentFilter<EnumValuesDocumentFilter>();
    opt.OperationFilter<Realtea.Api.Filters.SecurityRequirementsOperationFilter>();
}).AddSwaggerExamplesFromAssemblyOf(typeof(Program));

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
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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
builder.Services.AddScoped<IAuthorizationHandler, IsEligibleForAdvertisementUpdateAuthHandler>();
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
