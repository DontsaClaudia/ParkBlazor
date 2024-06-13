using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using ParkBlazor.Data;
using ParkBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();  
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAuthorizationCore();

// Register HttpClient for the API using configuration
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5165") });

// Register AuthService
builder.Services.AddScoped<AuthenticationService>();

// Register other services
builder.Services.AddScoped<ComputersService>();
builder.Services.AddScoped<RoomsService>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<ParksService>();
builder.Services.AddScoped<UsersService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  // Ajout du mappage des contrôleurs
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();