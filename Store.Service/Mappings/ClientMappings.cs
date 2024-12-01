using Store.Repository.Models;
using Store.Service.Models;

namespace Store.Service.Mappings;

public static class ClientMappings
{
    public static ClientEntity ToEntity(Client domain)
    {
        return new ClientEntity
        {
            Nif = domain.Nif,
            Name = domain.Name,
            Address = domain.Address
        };
    }

    public static Client ToDomainModel(ClientEntity entity)
    {
        return new Client
        {
            Nif = entity.Nif,
            Name = entity.Name,
            Address = entity.Address
        };
    }
}