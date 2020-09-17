using DroneDelivery.Shared.Domain.Core.Enums;
using System;

namespace DroneDelivery.Shared.Domain.Core.Events.Pedidos
{
    public class PedidoAtualizadoEvent : Event
    {
        public Guid Id { get; }

        public PedidoStatus Status { get; }

        public PedidoAtualizadoEvent(Guid id, PedidoStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}
