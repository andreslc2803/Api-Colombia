using ApiColombia.BL;
using ApiColombia.BL.Interfaces;
using ApiColombia.DAL.Data;
using ApiColombia.DAL.Repository;
using ApiColombia.DAL.Repository.Interfaces;
using ApiColombia.Entities.DTO;
using ApiColombia.Middleware;
using ApiColombia.Services;
using ApiColombia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiColombiaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient("RegionServiceApi", client =>
{
    client.BaseAddress = new Uri("https://api-colombia.com/api/v1/");
});

builder.Services.AddScoped<IExternalRegionService, ExternalRegionService>();
builder.Services.AddScoped<IApiColombiaRepository, ApiColombiaRepository>();
builder.Services.AddScoped<IApiColombiaServices, ApiColombiaServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
