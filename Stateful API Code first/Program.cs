using Microsoft.EntityFrameworkCore;
using Stateful_API_Code_first.Models.DataManagement;
using Stateful_API_Code_first.Models.Repository;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<Stateful_API_Code_first.Models.FilmRatingsDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISeriesManager, SeriesManager>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();