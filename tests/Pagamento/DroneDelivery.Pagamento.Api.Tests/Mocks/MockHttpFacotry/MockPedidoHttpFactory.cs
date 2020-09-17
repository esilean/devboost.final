using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using DroneDelivery.Shared.Infra.Interfaces;
using System.Threading.Tasks;

namespace DroneDelivery.Pagamento.Api.Tests.Mocks.MockHttpFacotry
{
    public class MockPedidoHttpFactory : IPedidoHttpFactory
    {
        public Task<bool> AtualizarPedidoStatus(string email, string password, PedidoAtualizadoEvent @event)
        {
            return Task.FromResult(true);
        }

        public Task<bool> EnviarPedidoParaEntrega(string email, string password, PedidoCriadoEvent @event)
        {
            return Task.FromResult(true);
        }
    }
}
