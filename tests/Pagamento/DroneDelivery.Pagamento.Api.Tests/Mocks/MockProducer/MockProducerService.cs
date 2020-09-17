using DroneDelivery.Shared.Domain.Core.Events.Interfaces;
using System.Threading.Tasks;

namespace DroneDelivery.Pagamento.Api.Tests.Mocks.MockProducer
{
    public class MockProducerService : IProducerService
    {

        public async Task PublicarMensagem(string topic, string value, string key)
        {
            await Task.CompletedTask;
        }
    }
}
