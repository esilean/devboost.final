using DroneDelivery.Shared.Domain.Core.Events.Pedidos;
using DroneDelivery.Shared.Infra.Interfaces;
using DroneDelivery.Shared.Utility.Dtos;
using DroneDelivery.Shared.Utility.Events;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DroneDelivery.Shared.Infra.HttpFactories
{
    public class PedidoHttpFactory : IPedidoHttpFactory
    {
        private readonly HttpClient _client;

        public PedidoHttpFactory(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(HttpClientName.PedidoEndPoint);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> EnviarPedidoParaEntrega(string email, string password, PedidoCriadoEvent @event)
        {
            await RealizarLogin(email, password);

            var response = await _client.PostAsJsonAsync("/api/pedidos", @event);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarPedidoStatus(string email, string password, PedidoAtualizadoEvent @event)
        {
            await RealizarLogin(email, password);

            var response = await _client.PostAsJsonAsync("/api/pedidos/atualizarstatus", @event);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        private async Task RealizarLogin(string email, string password)
        {
            var loginResponse = await _client.PostAsJsonAsync("/api/usuarios/login", new
            {
                email,
                password
            });

            loginResponse.EnsureSuccessStatusCode();
            var data = await loginResponse.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<JwtDto>(data);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Jwt.Token);
        }
    }
}
