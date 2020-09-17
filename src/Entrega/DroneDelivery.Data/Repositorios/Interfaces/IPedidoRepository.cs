using DroneDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Data.Repositorios.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {

        Task<IEnumerable<Pedido>> ObterDoClienteAsync(Guid usuarioId);

        Task<Pedido> ObterPorIdAsync(Guid pedidoId);

        Task AdicionarAsync(Pedido drone);

        Task AtualizarAsync(Pedido drone);

    }
}
