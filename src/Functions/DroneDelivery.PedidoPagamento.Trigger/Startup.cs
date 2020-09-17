using DroneDelivery.PedidoPagamento.Trigger.Services;
using DroneDelivery.Shared.Infra.Clients;
using DroneDelivery.Shared.Infra.HttpFactories;
using DroneDelivery.Shared.Infra.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(DroneDelivery.PedidoPagamento.Trigger.Startup))]
namespace DroneDelivery.PedidoPagamento.Trigger
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            HttpPagamentoClient.Registrar(builder.Services, Environment.GetEnvironmentVariable("UrlBasePagamento"));

            builder.Services.AddSingleton<IPedidoPagamentoService, PedidoPagamentoService>();
            builder.Services.AddSingleton<IPagamentoHttpFactory, PagamentoHttpFactory>();
        }
    }
}
