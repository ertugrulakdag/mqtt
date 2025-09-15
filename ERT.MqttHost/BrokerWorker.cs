using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERT.MqttHost
{
    public class BrokerWorker : BackgroundService
    {
        private readonly ILogger<BrokerWorker> _logger;
        private readonly IOptionsMonitor<MqttOptions> _optionsMonitor;

        public BrokerWorker(ILogger<BrokerWorker> logger, IOptionsMonitor<MqttOptions> optionsMonitor)
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // İlk okuma
            var opts = _optionsMonitor.CurrentValue;
            if (!Validate(opts, out var error))
            {
                _logger.LogError("MqttBroker ayar hatası: {Error}", error);
                return; // yanlış ayarlarla koşma
            }

            _logger.LogInformation("MqttBrokerWorker starting with Port={Port}, Username={User}", opts.Port, opts.Username);

            // Canlı ayar değişikliğini dinle (appsettings değişirse)
            _optionsMonitor.OnChange(o =>
            {
                if (Validate(o, out var err))
                {
                    _logger.LogInformation("MqttBroker options changed -> Port={Port}, Username={User}",o.Port, o.Username ?? "<none>");
                    // İleride burada broker yeniden başlatılabilir
                }
                else
                {
                    _logger.LogWarning("MqttBroker options change ignored (invalid): {Err}", err);
                }
            });

            // Şimdilik MQTT başlatmıyoruz: sadece heartbeat
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("MqttBrokerWorker heartbeat at {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private static bool Validate(MqttOptions o, out string? error)
        {
            if (o.Port <= 0 || o.Port > 65535)
            {
                error = $"Port değeri geçersiz: {o.Port}";
                return false;
            }
            error = null;
            return true;
        }
    }
}
