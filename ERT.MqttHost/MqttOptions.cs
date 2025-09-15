namespace ERT.MqttHost
{
    public class MqttOptions
    {
        public int Port { get; set; } = 1883;
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
