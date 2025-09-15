using ERT.MqttHost;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<BrokerWorker>();
builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttBroker"));

var host = builder.Build();
host.Run();
