using Confluent.Kafka;

namespace DroneDelivery.PedidoProducer.Config
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public Acks Acks { get; set; }
        public bool EnableIdempotence { get; set; }
        public int MessageSendMaxRetries { get; set; }
        public int MaxInFlight { get; set; }
        public CompressionType CompressionType { get; set; }
        public int LingerMs { get; set; }
        public int BatchSizeKB { get; set; }
        public string Topic { get; set; }
    }
}
