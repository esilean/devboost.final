using DroneDelivery.PedidoEntrega.Trigger.Config;
using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using DroneDelivery.Shared.Infra.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoEntrega.Trigger.Services
{
    public class PedidoEntregaService : IPedidoEntregaService
    {

        private readonly IPedidoHttpFactory _pedidoHttpFactory;
        private readonly AppConfig _appConfig;

        public PedidoEntregaService(IPedidoHttpFactory pedidoHttpFactory, IOptions<AppConfig> options)
        {
            _pedidoHttpFactory = pedidoHttpFactory;
            _appConfig = options.Value;
        }

        public async Task EnviarPedidoAsync(PedidoCriadoEvent @event)
        {
            await _pedidoHttpFactory.EnviarPedidoParaEntrega(_appConfig.Login, _appConfig.Senha, @event);
        }

        public async Task AtualizarPedidoAsync(PedidoAtualizadoEvent @event)
        {
            await _pedidoHttpFactory.AtualizarPedidoStatus(_appConfig.Login, _appConfig.Senha, @event);
        }

    }

}
