using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using DroneDelivery.Shared.Infra.Interfaces;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoPagamento.Trigger.Services
{
    public class PedidoPagamentoService : IPedidoPagamentoService
    {
        private readonly IPagamentoHttpFactory _pagamentoHttpFactory;

        public PedidoPagamentoService(IPagamentoHttpFactory pagamentoHttpFactory)
        {
            _pagamentoHttpFactory = pagamentoHttpFactory;
        }
        public async Task EnviarPedidoAsync(PedidoCriadoEvent @event)
        {
            await _pagamentoHttpFactory.EnviarPedidoParaPagamento(@event);
        }
    }
}
