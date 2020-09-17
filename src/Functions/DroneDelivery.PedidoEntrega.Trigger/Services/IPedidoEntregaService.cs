using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoEntrega.Trigger.Services
{
    public interface IPedidoEntregaService
    {
        Task EnviarPedidoAsync(PedidoCriadoEvent @event);

        Task AtualizarPedidoAsync(PedidoAtualizadoEvent @event);
    }
}
