using DroneDelivery.Shared.Domain.Core.Bus;
using DroneDelivery.Shared.Domain.Core.Commands;
using DroneDelivery.Shared.Domain.Core.Domain;
using DroneDelivery.Shared.Domain.Core.Events;
using DroneDelivery.Shared.Domain.Core.Events.Interfaces;
using DroneDelivery.Shared.Domain.Core.Queries;
using MediatR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DroneDelivery.Shared.Bus
{
    public class MediatorHandler : IEventBus
    {

        private readonly IMediator _mediator;
        private readonly IProducerService _producerService;

        public MediatorHandler(IMediator mediator,
                               IProducerService producerService)
        {
            _mediator = mediator;
            _producerService = producerService;
        }

        public Task<ResponseResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task<ResponseResult> RequestQuery<T>(T query) where T : Query
        {
            return _mediator.Send(query);
        }

        public async Task Publish<T>(string topic, T @event, string key) where T : Event
        {
            var value = JsonConvert.SerializeObject(@event);
            await _producerService.PublicarMensagem(topic, value, key);
        }
    }
}
