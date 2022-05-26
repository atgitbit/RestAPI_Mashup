using Microsoft.EntityFrameworkCore;
using RestAPI_Mashup;
using RestAPI_Mashup.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ArtistApiDBContext>(options => options.UseInMemoryDatabase("ArtistApiDB")); // detta är bara fö test "inmemory"
builder.Services.AddSingleton<IDbClient, DbClient>();
builder.Services.Configure<ArtistDBconfig>(builder.Configuration.GetSection("ArtistApiDB"));

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

app.Run();
