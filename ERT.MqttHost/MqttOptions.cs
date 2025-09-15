namespace ERT.MqttHost
{
    public class MqttOptions
    {
        public int Port { get; set; } = 1883;
        public string BindAddress { get; set; } = "0.0.0.0";
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? MaxConcurrentConnections { get; set; } = null; // null = sınırsız
        public int SessionExpirySeconds { get; set; } = 0; // 0 = temiz oturum

    }
}
