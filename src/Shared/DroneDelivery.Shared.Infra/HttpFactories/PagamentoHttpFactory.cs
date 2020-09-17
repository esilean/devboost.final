using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using DroneDelivery.Shared.Infra.Interfaces;
using DroneDelivery.Shared.Utility.Events;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DroneDelivery.Shared.Infra.HttpFactories
{
    public class PagamentoHttpFactory : IPagamentoHttpFactory
    {

        private readonly HttpClient _client;

        public PagamentoHttpFactory(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(HttpClientName.PagamentoEndPoint);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> EnviarPedidoParaPagamento(PedidoCriadoEvent @event)
        {
            var response = await _client.PostAsJsonAsync("/api/pedidos", @event);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

    }
}
