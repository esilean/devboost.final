using DroneDelivery.Shared.Domain.Core.Commands;
using DroneDelivery.Shared.Utility;
using Flunt.Validations;
using Newtonsoft.Json;
using System;

namespace DroneDelivery.Application.Commands.Pedidos
{
    public class CriarPedidoCommand : Command
    {
        public Guid Id { get; }

        public double Peso { get; }

        public double Valor { get; }


        [JsonConstructor]
        public CriarPedidoCommand(Guid id, double peso, double valor)
        {
            Id = id;
            Peso = peso;
            Valor = valor;
        }

        public void Validate()
        {

            AddNotifications(new Contract()
                .Requires()
                .IsNotEmpty(Id, nameof(Id), "O Id do Pedido não pode ser vazio"));

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Peso, 0, nameof(Peso), "O Peso tem que ser maior que zero"));

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Valor, 0, nameof(Valor), "O Valor tem que ser maior que zero"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(Peso, Utils.CAPACIDADE_MAXIMA_GRAMAS, nameof(Peso), $"O Peso tem que ser menor ou igual a {Utils.CAPACIDADE_MAXIMA_GRAMAS / 1000} KGs"));

        }
    }
}
