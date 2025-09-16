using MQTTnet;
using System.Text;

namespace ERT.Shared.Services
{
    public class MqttService
    {
        IMqttClient? mqttClient;
        public async Task Start(string brokerIp, string clientId,string topic ,string username, string password, Action<string>? callback = null)
        {
            var factory = new MqttClientFactory();

            var options = new MqttClientOptionsBuilder().WithTcpServer(brokerIp).WithClientId(clientId).WithCredentials(username, password).WithCleanSession().Build();

            mqttClient = factory.CreateMqttClient();

            mqttClient.ConnectedAsync += (async e =>
            {
                Console.WriteLine("MQTT connected");
                await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            });

            mqttClient.ApplicationMessageReceivedAsync += (async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine("Received MQTT message");
                Console.WriteLine($" - Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($" - Payload = {payload}");
                Console.WriteLine($" - QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($" - Retain = {e.ApplicationMessage.Retain}");

                callback?.Invoke(payload);
            });

            mqttClient.DisconnectedAsync += (async e =>
            {
                Console.WriteLine("MQTT reconnecting");
                await Task.Delay(TimeSpan.FromSeconds(5));
                await mqttClient.ConnectAsync(options, CancellationToken.None);
            });

            await mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        public async Task SendCode(string topic, string message)
        {
            Console.WriteLine("Publish MQTT message");
            Console.WriteLine($" - Topic: {topic}");
            Console.WriteLine($" - Payload: {message}");
            var applicationMessage = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(message).Build();
            if (mqttClient != null)
            {
                await mqttClient.PublishAsync(applicationMessage);
            }
        }
    }
}
