using System.Text.Json;

namespace ERT.Shared.Configuration
{
    public static class MqttConfig
    {
        /// <summary>
        /// appsettings.json içindeki sectionName'i okur.
        /// </summary>
        public static MqttClientSettings Read(string filePath = "appsettings.json", string sectionName = "MqttClient")
        {
            var json = File.ReadAllText(filePath);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement.GetProperty(sectionName);

            return new MqttClientSettings
            {
                ClientId = root.GetProperty("ClientId").GetString() ?? string.Empty,
                Username = root.GetProperty("Username").GetString() ?? string.Empty,
                Password = root.GetProperty("Password").GetString() ?? string.Empty,
                Host = root.GetProperty("Host").GetString() ?? "localhost",
                Port = root.TryGetProperty("Port", out var p) && p.TryGetInt32(out var port) ? port : 1883
            };
        }
    }
}
