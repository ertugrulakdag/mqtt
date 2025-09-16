using ERT.MqttClientWeb.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<MessageStore>();
builder.Services.AddHostedService<MqttBackgroundListener>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.MapGet("/api/messages", ([FromServices] MessageStore store, int? take) =>
{
    var list = store.GetLatest(take ?? 200);
    return Results.Ok(list);
});


app.Run();
