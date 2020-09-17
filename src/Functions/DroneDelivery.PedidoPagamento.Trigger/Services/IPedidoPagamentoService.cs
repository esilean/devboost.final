using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoPagamento.Trigger.Services
{
    public interface IPedidoPagamentoService
    {
        Task EnviarPedidoAsync(PedidoCriadoEvent @event);
    }
}
