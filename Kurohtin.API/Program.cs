using Microsoft.EntityFrameworkCore;
using Kurohtin.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString =
builder.Configuration.GetConnectionString("SqLiteConnection") ??
throw new InvalidOperationException("Connection string 'SqLiteConnection' not found.");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IProductService, ApiProductService>(opt
=> opt.BaseAddress = new Uri("https://localhost:7002/api/crepezhy/"));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt
=> opt.BaseAddress = new
Uri("https://localhost:7002/api/categories/"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

await DbInitializer.SeedData(app);

app.Run();
