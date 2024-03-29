using Ecommerce.Services.CatalogAPI.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
var keysFolder = builder.Configuration["DATA_PROTECTION_KEYS_FOLDER"];
if (!string.IsNullOrEmpty(keysFolder))
{
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(keysFolder));
}
builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(conn));

// Register the ICatalogRepository
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

builder.Services.AddControllers();
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

//try-catch database testi i�in eklenmi�tir. Silinecek!
try
{
    CatalogDbInit.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}


app.Run();
