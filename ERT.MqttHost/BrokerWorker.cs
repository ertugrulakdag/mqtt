using ERT.MqttHost;
using Microsoft.Extensions.Options;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Text;

public class BrokerWorker : BackgroundService
{
    private readonly ILogger<BrokerWorker> _logger;
    private readonly IOptionsMonitor<MqttOptions> _options;
    private MqttServer? _server;

    public BrokerWorker(ILogger<BrokerWorker> logger, IOptionsMonitor<MqttOptions> options)
    {
        _logger = logger;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = _options.CurrentValue;
        var serverFactory = new MqttServerFactory();

        var options = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithDefaultEndpointPort(config.Port)
            .Build();

        // Dinleme kuyruğu (backlog)
        if (config.MaxConcurrentConnections.HasValue)
        {
            options.DefaultEndpointOptions.ConnectionBacklog = config.MaxConcurrentConnections.Value;
        }

        var server = serverFactory.CreateMqttServer(options);

        server.ValidatingConnectionAsync += e =>
        {
            var ok = (e.UserName == config.Username) && (e.Password == config.Password);
            e.ReasonCode = ok ? MqttConnectReasonCode.Success
                              : MqttConnectReasonCode.BadUserNameOrPassword;

            if (ok)
                _logger.LogInformation("AUTH OK : clientId={ClientId} user={User}", e.ClientId, e.UserName);
            else
                _logger.LogWarning("AUTH RED: clientId={ClientId} user={User}", e.ClientId, e.UserName);

            return Task.CompletedTask;
        };

        server.ClientConnectedAsync += e =>
        {
            _logger.LogInformation("[CONNECT] {ClientId}", e.ClientId);
            return Task.CompletedTask;
        };

        server.ClientDisconnectedAsync += e =>
        {
            _logger.LogInformation("[DISCONNECT] {ClientId} - {Type}", e.ClientId, e.DisconnectType);
            return Task.CompletedTask;
        };

        server.ClientSubscribedTopicAsync += e =>
        {
            _logger.LogInformation("[SUBSCRIBE] {ClientId} -> {Topic}", e.ClientId, e.TopicFilter.Topic);
            return Task.CompletedTask;
        };

        server.InterceptingPublishAsync += e =>
        {
            var payload = e.ApplicationMessage?.Payload is { Length: > 0 } p
                ? Encoding.UTF8.GetString(p)
                : string.Empty;

            _logger.LogInformation("[PUBLISH] from={Client} topic={Topic} qos={Qos} retain={Retain} payload='{Payload}'",
                e.ClientId,
                e.ApplicationMessage?.Topic,
                e.ApplicationMessage?.QualityOfServiceLevel,
                e.ApplicationMessage?.Retain,
                payload);

            return Task.CompletedTask;
        };

        await server.StartAsync();
        _server = server;

        _logger.LogInformation($"MQTT Broker hazır: tcp://{config.BindAddress}:{config.Port} (Windows Service)");

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError("[ERROR] ExceptionMessage:{ex}", ex.Message);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_server != null)
        {
            try
            {
                await _server.StopAsync();  
                _server.Dispose();
                _logger.LogInformation("Broker durdu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Broker durdurulurken hata");
            }
            finally
            {
                _server = null;
            }
        }

        await base.StopAsync(cancellationToken);
    }
}
