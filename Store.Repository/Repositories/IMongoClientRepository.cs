using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface IMongoClientRepository
{
    Task AddClient(ClientEntity client);

    Task<ClientEntity?> GetClientByNif(string nif);
}