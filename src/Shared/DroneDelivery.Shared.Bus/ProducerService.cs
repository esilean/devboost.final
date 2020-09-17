using Confluent.Kafka;
using DroneDelivery.Shared.Domain.Core.Events.Interfaces;
using System;
using System.Threading.Tasks;

namespace DroneDelivery.Shared.Bus
{
    public class ProducerService : IProducerService
    {

        private readonly IProducer<string, string> _producer;

        public ProducerService(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublicarMensagem(string topic, string value, string key)
        {
            var message = new Message<string, string>
            {
                Key = key,
                Value = value
            };

            try
            {
                await _producer.ProduceAsync(topic, message);
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Falha ao entregar mensagem: {e.Error.Reason}");
            }
        }

    }
}
