using DroneDelivery.Shared.Domain.Core.Domain;
using DroneDelivery.Shared.Domain.Core.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DroneDelivery.Domain.Models
{
    public class Pedido : Entity, IAggregateRoot
    {

        private readonly List<HistoricoPedido> _historicoPedidos = new List<HistoricoPedido>();

        public Guid UsuarioId { get; private set; }

        [BsonIgnore]
        public Usuario Usuario { get; private set; }

        public double Peso { get; private set; }
        public DateTime DataPedido { get; private set; }
        public double Valor { get; private set; }
        public PedidoStatus Status { get; private set; }

        public Guid? DroneId { get; private set; }

        [BsonIgnore]
        public Drone Drone { get; private set; }

        public IReadOnlyCollection<HistoricoPedido> HistoricoPedidos => _historicoPedidos;

        protected Pedido() { }

        private Pedido(Guid id, double peso, double valor, Usuario usuario)
        {
            Id = id;
            Peso = peso;
            Valor = valor;
            UsuarioId = usuario.Id;
            DataPedido = DateTime.Now;
            Status = PedidoStatus.Pendente;
        }

        public static Pedido Criar(Guid id, double peso, double valor, Usuario usuario)
        {
            return new Pedido(id, peso, valor, usuario);
        }

        public void AtualizarStatusPedido(PedidoStatus status)
        {
            Status = status;
        }

        public void AssociarDrone(Guid droneId)
        {
            DroneId = droneId;
        }

        public void CriarHistorico(HistoricoPedido historicoPedido)
        {
            _historicoPedidos.Add(historicoPedido);
        }

        public void CarregarUsuario(Usuario usuario)
        {
            Usuario = usuario;
            UsuarioId = usuario.Id;
        }

    }
}
