using DroneDelivery.Data.Data;
using DroneDelivery.Data.Repositorios.Interfaces;
using DroneDelivery.Entrega.Data.Data;
using System.Threading.Tasks;

namespace DroneDelivery.Data.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DroneDbContext _context;
        private readonly DroneMongoDbContext _contextMongo;

        public UnitOfWork(DroneDbContext context, DroneMongoDbContext contextMongo)
        {
            _context = context;
            _contextMongo = contextMongo;

            Pedidos = new PedidoRepository(_context, _contextMongo);
            Drones = new DroneRepository(_context, _contextMongo);
            Usuarios = new UsuarioRepository(_context);
        }


        public IPedidoRepository Pedidos { get; private set; }

        public IDroneRepository Drones { get; private set; }

        public IUsuarioRepository Usuarios { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
