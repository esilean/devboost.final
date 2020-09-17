using System.Threading.Tasks;

namespace DroneDelivery.Shared.Domain.Core.Events.Interfaces
{
    public interface IProducerService
    {
        Task PublicarMensagem(string topic, string value, string key);
    }
}
