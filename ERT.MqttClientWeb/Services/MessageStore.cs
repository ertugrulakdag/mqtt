using ERT.MqttClientWeb.Model;
using System.Collections.Concurrent;

namespace ERT.MqttClientWeb.Services
{
    public class MessageStore
    {
        private readonly ConcurrentQueue<MqttUiMessage> _queue = new();
        private const int MaxItems = 100; // hafızada tutulacak maksimum satır

        public void Add(MqttUiMessage msg)
        {
            _queue.Enqueue(msg);
            while (_queue.Count > MaxItems && _queue.TryDequeue(out _)) { }
        }

        public IReadOnlyList<MqttUiMessage> GetLatest(int take = 100)
        {
            var arr = _queue.ToArray();
            return arr.Reverse().Take(take).ToList();
        }
    }
}
