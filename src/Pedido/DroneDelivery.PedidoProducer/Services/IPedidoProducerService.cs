using DroneDelivery.PedidoProducer.Dto;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoProducer.Services
{
    public interface IPedidoProducerService
    {
        Task EnviarPedido(CriarPedidoDto pedidoDto);
    }
}
