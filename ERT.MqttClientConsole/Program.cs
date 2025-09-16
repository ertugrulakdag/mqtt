using ERT.Shared.Configuration;
using ERT.Shared.Services;

var mqttSettings = MqttConfig.Read();
Console.WriteLine($"ClientId:{mqttSettings.ClientId} Host:{mqttSettings.Host} Port:{mqttSettings.Port} Baslatiliyor...");

MqttService mqttService = new();

await mqttService.Start(mqttSettings.Host, mqttSettings.ClientId, "topic-name", mqttSettings.Username, mqttSettings.Password, async (message) =>
{
    Console.WriteLine($"client:{mqttSettings.ClientId} mesaj:{message}");
});
 
Console.ReadLine();
