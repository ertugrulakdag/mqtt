using ERT.MqttClientWeb.Model;
using ERT.Shared.Configuration;
using ERT.Shared.Services;

namespace ERT.MqttClientWeb.Services
{
    public class MqttBackgroundListener : BackgroundService
    {
        private readonly MessageStore _store;
        private readonly ILogger<MqttBackgroundListener> _logger;

        public MqttBackgroundListener(MessageStore store, ILogger<MqttBackgroundListener> logger)
        {
            _store = store;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttSettings = MqttConfig.Read();
            Console.WriteLine($"ClientId:{mqttSettings.ClientId} Host:{mqttSettings.Host} Port:{mqttSettings.Port} Baslatiliyor...");

            var mqttService = new MqttService();

            await mqttService.Start(
                mqttSettings.Host,
                mqttSettings.ClientId,
                "topic-name", 
                mqttSettings.Username,
                mqttSettings.Password,
                async (message) =>
                {
                    var payload = message?.ToString() ?? "";

                    var dto = new MqttUiMessage
                    {
                        Topic = "topic-name",
                        Payload = payload,
                        ReceivedAt = DateTime.Now
                    };

                    _store.Add(dto);
                    _logger.LogDebug("MQTT msg alındı: {Payload}", dto.Payload);

                    await Task.CompletedTask;
                });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
