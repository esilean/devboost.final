using Confluent.Kafka;
using DroneDelivery.Pagamento.Application.CommandHandlers.Pagamentos;
using DroneDelivery.Pagamento.Application.CommandHandlers.Pedidos;
using DroneDelivery.Pagamento.Application.Commands.Pagamentos;
using DroneDelivery.Pagamento.Application.Commands.Pedidos;
using DroneDelivery.Pagamento.Application.Interfaces;
using DroneDelivery.Pagamento.Data.Data;
using DroneDelivery.Pagamento.Data.Repositorios;
using DroneDelivery.Pagamento.Data.Repositorios.Interfaces;
using DroneDelivery.Pagamento.Gateway;
using DroneDelivery.Shared.Bus;
using DroneDelivery.Shared.Domain.Core.Bus;
using DroneDelivery.Shared.Domain.Core.Domain;
using DroneDelivery.Shared.Domain.Core.Events.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DroneDelivery.Pagamento.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, ProducerConfig producerConfig)
        {
            //mediator
            services.AddScoped<IEventBus, MediatorHandler>();

            //events
            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<IProducerService, ProducerService>();

            //commands
            services.AddScoped<IRequestHandler<CriarPedidoCommand, ResponseResult>, CriarPedidoHandler>();
            services.AddScoped<IRequestHandler<CriarPagamentoCommand, ResponseResult>, CriarPagamentoHandler>();
            services.AddScoped<IRequestHandler<WebhookPagamentoCommand, ResponseResult>, WebhookPagamentoHandler>();

            //gateway
            services.AddScoped<IGatewayPagamento, GatewayPagamento>();

            //repositórios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DronePgtoDbContext>();
        }
    }
}
