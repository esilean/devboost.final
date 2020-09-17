using System;

namespace DroneDelivery.Shared.Domain.Core.Events.Pedidos
{
    public class PedidoCriadoEvent : Event
    {
        public Guid Id { get; }

        public double Peso { get; }

        public double Valor { get; }

        public PedidoCriadoEvent(Guid id, double peso, double valor)
        {
            Id = id;
            Peso = peso;
            Valor = valor;
        }
    }
}
