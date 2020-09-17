using DroneDelivery.Data.Data;
using DroneDelivery.Data.Repositorios;
using DroneDelivery.Data.Repositorios.Interfaces;
using DroneDelivery.Data.Tests.Config;
using DroneDelivery.Domain.Enum;
using DroneDelivery.Domain.Models;
using DroneDelivery.Entrega.Data.Data;
using DroneDelivery.Entrega.Data.Data.Config;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DroneDelivery.Data.Tests.Repositorios
{
    public class PedidoRepositoryTests : DbConfig
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly DroneDbContext _context;
        private readonly DroneMongoDbContext _contextMongo;

        public PedidoRepositoryTests()
        {
            _context = new DroneDbContext(ContextOptions);
            _contextMongo = new DroneMongoDbContext(MongoOptions);
            _pedidoRepository = new PedidoRepository(_context, _contextMongo);
        }

        [Fact(DisplayName = "Salvar pedido no banco de dados")]
        public async Task Pedido_QuandoSalvarPedido_DeveExistirNoBancoDados()
        {
            //Arrange
            var usuario = Usuario.Criar("test", "test@email", 0, 0, UsuarioRole.Cliente);
            usuario.AdicionarPassword("password");
            usuario.AdicionarRefreshToken("refreshToken", DateTime.Now.AddDays(1));
            await _context.AddAsync(usuario);

            var pedido = Pedido.Criar(Guid.NewGuid(), 999, 1000, usuario);
            pedido.CarregarUsuario(usuario);

            //Act
            await _pedidoRepository.AdicionarAsync(pedido);

            //Assert
            var pedidoResult = _contextMongo.Pedidos.Find(x => x.Id == pedido.Id).FirstOrDefault(); ;
            Assert.Equal(pedido.Peso, pedidoResult.Peso);
            Assert.Equal(pedido.UsuarioId, pedidoResult.UsuarioId);
        }

        [Fact(DisplayName = "Obter pedidos do cliente do banco de dados")]
        public async Task Pedido_QuandoExistiremPedidos_DeveRetornarDoBancoDados()
        {
            //Arrange
            var usuario = Usuario.Criar("test", "test@email", 0, 0, UsuarioRole.Cliente);
            usuario.AdicionarPassword("password");
            usuario.AdicionarRefreshToken("refreshToken", DateTime.Now.AddDays(1));
            await _context.AddAsync(usuario);

            var pedido1 = Pedido.Criar(Guid.NewGuid(), 123, 1000, usuario);
            pedido1.CarregarUsuario(usuario);
            var pedido2 = Pedido.Criar(Guid.NewGuid(), 456, 1000, usuario);
            pedido2.CarregarUsuario(usuario);

            await _pedidoRepository.AdicionarAsync(pedido1);
            await _pedidoRepository.AdicionarAsync(pedido2);

            //Act
            var pedidos = await _pedidoRepository.ObterDoClienteAsync(usuario.Id);

            //Assert
            var pedidoResult = await _contextMongo.Pedidos.Find(new BsonDocument()).ToListAsync();
            Assert.NotNull(pedidos.FirstOrDefault(x => x.Peso == pedidoResult.FirstOrDefault(x => x.Peso == 123).Peso));
            Assert.NotNull(pedidos.FirstOrDefault(x => x.Peso == pedidoResult.FirstOrDefault(x => x.Peso == 456).Peso));
        }

    }
}
