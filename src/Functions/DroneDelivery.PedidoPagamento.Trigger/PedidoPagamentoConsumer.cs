using DroneDelivery.PedidoPagamento.Trigger.Services;
using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoPagamento.Trigger
{
    public class PedidoPagamentoConsumer
    {
        private readonly IPedidoPagamentoService _pedidoPagamentoService;

        public PedidoPagamentoConsumer(IPedidoPagamentoService pedidoPagamentoService)
        {
            _pedidoPagamentoService = pedidoPagamentoService;
        }

        [FunctionName(nameof(PedidoPagamentoConsumer))]
        public async Task PedidoPagamentoTrigger(
            [KafkaTrigger(
            "%BootstrapServers%",
            "%TopicPedidoCriado%",
            ConsumerGroup = "%ConsumerGroup%")]
            KafkaEventData<string> kafkaEvent,
            ILogger logger)
        {
            logger.LogInformation(kafkaEvent.Value.ToString());

            try
            {
                var @event = JsonConvert.DeserializeObject<PedidoCriadoEvent>(kafkaEvent.Value);
                await _pedidoPagamentoService.EnviarPedidoAsync(@event);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
