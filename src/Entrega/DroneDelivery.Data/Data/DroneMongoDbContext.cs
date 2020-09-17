using DroneDelivery.Domain.Models;
using DroneDelivery.Entrega.Data.Data.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DroneDelivery.Entrega.Data.Data
{
    [ExcludeFromCodeCoverage]
    public class DroneMongoDbContext
    {

        private readonly IMongoDatabase _database;

        public DroneMongoDbContext(IOptions<MongoDbConfig> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            _database = client.GetDatabase(config.Value.Database);
        }

        public IMongoCollection<Pedido> Pedidos => _database.GetCollection<Pedido>("Pedidos");

    }
}
