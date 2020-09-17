using DroneDelivery.Data.Data;
using DroneDelivery.Data.Repositorios.Interfaces;
using DroneDelivery.Domain.Enum;
using DroneDelivery.Domain.Models;
using DroneDelivery.Entrega.Data.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Data.Repositorios
{
    public class DroneRepository : IDroneRepository
    {
        private readonly DroneDbContext _context;
        private readonly DroneMongoDbContext _contextMongo;

        public DroneRepository(DroneDbContext context, DroneMongoDbContext contextMongo)
        {
            _context = context;
            _contextMongo = contextMongo;
        }
        public async Task AdicionarAsync(Drone drone)
        {
            await _context.Drones.AddAsync(drone);
        }

        public async Task<IEnumerable<Drone>> ObterTodosAsync()
        {
            var drones = await _context.Drones.ToListAsync();

            var pedidos = await _contextMongo.Pedidos.Find(new BsonDocument()).ToListAsync();
            foreach (var pedido in pedidos)
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == pedido.UsuarioId);
                if (usuario != null)
                    pedido.CarregarUsuario(usuario);
            }

            foreach (var drone in drones)
            {
                drone.CarregarPedidos(pedidos.Where(x => x.DroneId == drone.Id));
            }

            return drones;
        }

        public async Task<IEnumerable<Drone>> ObterDronesParaEntregaAsync()
        {
            var drones = await _context.Drones.Where(x => x.Status == DroneStatus.Livre).ToListAsync();

            var pedidos = await _contextMongo.Pedidos.Find(new BsonDocument()).ToListAsync();
            foreach (var pedido in pedidos)
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == pedido.UsuarioId);
                if (usuario != null)
                    pedido.CarregarUsuario(usuario);
            }

            foreach (var drone in drones)
            {
                drone.CarregarPedidos(pedidos.Where(x => x.DroneId == drone.Id));
            }

            return drones.Where(x => x.Pedidos.Count() > 0);
        }

        public async Task AdicionarHistoricoAsync(IEnumerable<HistoricoPedido> historicoPedidos)
        {
            await _context.AddRangeAsync(historicoPedidos);
        }
    }
}
