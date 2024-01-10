using Ecommerce.Services.CustomerAPI.Data;
using Ecommerce.Services.CustomerAPI.Models;
using Ecommerce.Services.CustomerAPI.Service;
using Ecommerce.Services.CustomerAPI.Service.IService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
var keysFolder = builder.Configuration["DATA_PROTECTION_KEYS_FOLDER"];
if (!string.IsNullOrEmpty(keysFolder))
{
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(keysFolder));
}

builder.Services.AddDbContext<CustomerDbContext>(options => options.UseNpgsql(conn));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<CustomerUser, IdentityRole>().AddEntityFrameworkStores<CustomerDbContext>()
    .AddDefaultTokenProviders(); 
builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
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

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
