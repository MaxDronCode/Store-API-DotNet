using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Repository.DbConfig;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class MongoClientRepository : IMongoClientRepository
{
    private readonly IMongoCollection<ClientEntity> _clients;

    public MongoClientRepository(IMongoClient mongoClient, IOptions<MongoDbConfig> config)
    {
        var database = mongoClient.GetDatabase(config.Value.DatabaseName);
        _clients = database.GetCollection<ClientEntity>("clients");
    }

    public async Task AddClient(ClientEntity entity)
    {
        await _clients.InsertOneAsync(entity);
    }

    public async Task<ClientEntity?> GetClientByNif(string nif)
    {
        return await _clients.Find(c => c.Nif == nif).FirstOrDefaultAsync();
    }
}