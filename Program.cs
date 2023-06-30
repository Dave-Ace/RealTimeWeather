
using Microsoft.AspNetCore.SignalR;
using RealTimeWeather.Hubs;
using RealTimeWeather.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

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

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<NotificationHub>("notifications-hub");

app.MapPost("notifications/all", async (
    string content,
    IHubContext<NotificationHub, INotificationClient> context) =>
    {
        await context.Clients.All.ReceiveNotification(content);

        return Results.NoContent();
    });

app.Run();
