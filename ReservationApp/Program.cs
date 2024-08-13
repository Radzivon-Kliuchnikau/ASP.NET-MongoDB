using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using ReservationApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseMongoDB(builder.Configuration["AtlasURI"] ?? string.Empty, mongoDbSettings.DatabaseName ?? string.Empty));

builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurant}/{action=Index}/{id?}");

app.Run();
