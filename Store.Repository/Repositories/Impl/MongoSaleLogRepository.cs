using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Repository.DbConfig;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class MongoSaleLogRepository : IMongoSaleLogRepository
{
    private readonly IMongoCollection<SaleLogEntity> _saleLogs;

    public MongoSaleLogRepository(IMongoClient mongoClient, IOptions<MongoDbConfig> config)
    {
        var database = mongoClient.GetDatabase(config.Value.DatabaseName);
        _saleLogs = database.GetCollection<SaleLogEntity>("saleLogs");
    }

    public async Task AddSaleLog(SaleLogEntity entity)
    {
        await _saleLogs.InsertOneAsync(entity);
    }

    public async Task<IEnumerable<SaleLogEntity>> GetSaleLogsByClientNif(string clientNif)
    {
        return await _saleLogs.Find(s => s.ClientNif == clientNif).ToListAsync();
    }
}