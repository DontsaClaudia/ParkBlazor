using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ParkBlazor.Data;
using ParkBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<ComputersService>();
builder.Services.AddScoped<RoomsService>();

builder.Services.AddHttpClient<ParksService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5124/"); // Route API de parks
});
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
