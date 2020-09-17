using Confluent.Kafka;
using DroneDelivery.PedidoProducer.Config;
using DroneDelivery.PedidoProducer.Dto;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoProducer.Services
{
    public class PedidoProducerService : IPedidoProducerService
    {
        private readonly KafkaConfig _config;

        public PedidoProducerService(IOptions<KafkaConfig> options)
        {
            _config = options.Value;
        }


        public async Task EnviarPedido(CriarPedidoDto pedidoDto)
        {

            var config = new ProducerConfig { BootstrapServers = _config.BootstrapServers };

            //producer mais seguro
            config.Acks = _config.Acks;
            config.EnableIdempotence = _config.EnableIdempotence;
            config.MessageSendMaxRetries = _config.MessageSendMaxRetries;
            config.MaxInFlight = _config.MaxInFlight;

            //melhorar taxa de transferencia
            config.CompressionType = _config.CompressionType;
            config.LingerMs = _config.LingerMs;
            config.BatchSize = _config.BatchSizeKB * 1024;

            using var producer = new ProducerBuilder<int, string>(config).Build();
            try
            {
                var value = JsonConvert.SerializeObject(pedidoDto);

                await producer.ProduceAsync(
                    _config.Topic,
                    new Message<int, string> { Key = new Random().Next(0, 2), Value = value });
            }
            catch (ProduceException<int, string> e)
            {
                Console.WriteLine($"Falha ao entregar a mensagem: {e.Message} [{e.Error.Code}]");
            }
        }
    }
}
