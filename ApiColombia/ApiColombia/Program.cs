using ApiColombia.BL;
using ApiColombia.BL.AuthService;
using ApiColombia.BL.AuthService.Interfaces;
using ApiColombia.BL.Services.Interfaces;
using ApiColombia.DAL.Data;
using ApiColombia.DAL.Repository.ApiColombia;
using ApiColombia.DAL.Repository.ApiColombia.Interfaces;
using ApiColombia.DAL.Repository.Auth;
using ApiColombia.DAL.Repository.Auth.Interfaces;
using ApiColombia.Entities.DTO;
using ApiColombia.Entities.DTO.Security;
using ApiColombia.Middleware;
using ApiColombia.Security;
using ApiColombia.Services;
using ApiColombia.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Config swagger 

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiColombia",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT así: Bearer {su token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

builder.Services.AddDbContext<ApiColombiaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

# region JWT

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings!.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();

#endregion

# region External Services

builder.Services.AddHttpClient("RegionServiceApi", client =>
{
    client.BaseAddress = new Uri("https://api-colombia.com/api/v1/");
});

#endregion

# region DI

builder.Services.AddScoped<IExternalRegionService, ExternalRegionService>();
builder.Services.AddScoped<IApiColombiaRepository, ApiColombiaRepository>();
builder.Services.AddScoped<IApiColombiaServices, ApiColombiaServices>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

#endregion


#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
