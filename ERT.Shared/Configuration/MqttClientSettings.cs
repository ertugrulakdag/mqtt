namespace ERT.Shared.Configuration
{
    public sealed class MqttClientSettings
    {
        public required string ClientId { get; set; }  
        public required string Username { get; set; } 
        public required string Password { get; set; } 
        public required string Host { get; set; }  
        public int Port { get; set; } 
        
    }
}
