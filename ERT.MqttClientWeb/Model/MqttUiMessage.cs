namespace ERT.MqttClientWeb.Model
{
    public class MqttUiMessage
    {
        public string? Topic { get; set; }  
        public string? Payload { get; set; }  
        public DateTime ReceivedAt { get; set; }
    }
}
