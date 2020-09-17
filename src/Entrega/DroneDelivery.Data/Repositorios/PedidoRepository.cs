using DroneDelivery.Data.Data;
using DroneDelivery.Data.Repositorios.Interfaces;
using DroneDelivery.Domain.Models;
using DroneDelivery.Entrega.Data.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneDelivery.Data.Repositorios
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DroneDbContext _context;
        private readonly DroneMongoDbContext _contextMongo;

        public PedidoRepository(DroneDbContext context, DroneMongoDbContext contextMongo)
        {
            _context = context;
            _contextMongo = contextMongo;
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _contextMongo.Pedidos.InsertOneAsync(pedido);
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            await _contextMongo.Pedidos.ReplaceOneAsync(x => x.Id == pedido.Id, pedido);
        }


        public async Task<IEnumerable<Pedido>> ObterDoClienteAsync(Guid usuarioId)
        {
            return await _contextMongo.Pedidos.Find(x => x.UsuarioId == usuarioId).ToListAsync();
        }


        public async Task<Pedido> ObterPorIdAsync(Guid pedidoId)
        {
            var pedido = await _contextMongo.Pedidos.Find(x => x.Id == pedidoId).FirstOrDefaultAsync();

            if (pedido != null)
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == pedido.UsuarioId);
                if (usuario != null)
                    pedido.CarregarUsuario(usuario);
            }

            return pedido;
        }
    }
}
