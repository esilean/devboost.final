using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using System.Threading.Tasks;

namespace DroneDelivery.Shared.Infra.Interfaces
{
    public interface IPedidoHttpFactory
    {
        Task<bool> EnviarPedidoParaEntrega(string email, string password, PedidoCriadoEvent @event);

        Task<bool> AtualizarPedidoStatus(string email, string password, PedidoAtualizadoEvent @event);
    }
}
