using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;
using UrlShortner.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseInMemoryDatabase("UrlShortnerDb"));
builder.Services.AddSingleton<UrlService>();

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
