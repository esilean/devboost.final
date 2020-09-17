using DroneDelivery.PedidoEntrega.Trigger.Services;
using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace DroneDelivery.PedidoEntrega.Trigger
{
    public class PedidoEntregaConsumer
    {
        private readonly IPedidoEntregaService _pedidoEntregaService;

        public PedidoEntregaConsumer(IPedidoEntregaService pedidoEntregaService)
        {
            _pedidoEntregaService = pedidoEntregaService;
        }

        [FunctionName(nameof(PedidoEntregaTrigger))]
        public async Task PedidoEntregaTrigger(
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
                await _pedidoEntregaService.EnviarPedidoAsync(@event);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        [FunctionName(nameof(PedidoStatusTrigger))]
        public async Task PedidoStatusTrigger(
            [KafkaTrigger(
            "%BootstrapServers%",
            "%TopicPedidoAtualizado%",
            ConsumerGroup = "%ConsumerGroup%")]
            KafkaEventData<string> kafkaEvent,
            ILogger logger)
        {
            logger.LogInformation(kafkaEvent.Value.ToString());

            try
            {
                var @event = JsonConvert.DeserializeObject<PedidoAtualizadoEvent>(kafkaEvent.Value);
                await _pedidoEntregaService.AtualizarPedidoAsync(@event);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

    }
}
