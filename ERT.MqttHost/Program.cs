using ERT.MqttHost;
using Microsoft.Extensions.Hosting.WindowsServices;

var builder = Host.CreateApplicationBuilder(args);

var isWindowsService = WindowsServiceHelpers.IsWindowsService();

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Logging.AddEventLog(opt =>
{
    opt.SourceName = "ERT.MqttHost";
    opt.LogName = "Application";
});

if (!isWindowsService || builder.Environment.IsDevelopment())
{
    builder.Logging.AddSimpleConsole(o =>
    {
        o.SingleLine = true;
        o.TimestampFormat = "HH:mm:ss ";
        o.IncludeScopes = false;
    });
    builder.Logging.AddDebug();
}

builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttBroker"));
builder.Services.AddHostedService<BrokerWorker>();

builder.Services.AddWindowsService(o => o.ServiceName = "ERTMqttHost");

var app = builder.Build();
app.Run();
