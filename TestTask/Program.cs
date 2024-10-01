using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Services;
using TestTask.SignalRHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddSignalR();
builder.Services.AddScoped<Stats>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapHub<StatsHub>("/statshub");

//app.MapFallbackToFile("index.html");

app.Run();
