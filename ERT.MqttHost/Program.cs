using ERT.MqttHost;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(o => o.ServiceName = "ERT MQTT Host");

builder.Logging.ClearProviders();

builder.Logging.AddEventLog(opt =>
{
    opt.SourceName = "ERT.MqttHost";
    opt.LogName = "Application";
});

builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttBroker"));
builder.Services.AddHostedService<BrokerWorker>();

var host = builder.Build();
host.Run();
