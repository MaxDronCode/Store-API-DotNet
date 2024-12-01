using Store.Repository.Repositories;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Client AddClient(Client client)
    {
        var entity = ClientMappings.ToEntity(client);
        var addedEntity = _clientRepository.AddClient(entity);
        return ClientMappings.ToDomainModel(addedEntity);
    }

    public async Task<Client?> GetClientByNif(string nif)
    {
        var clientEntity = await _clientRepository.GetClientByNif(nif);
        return clientEntity != null ? ClientMappings.ToDomainModel(clientEntity) : null;
    }

    public IEnumerable<Client> GetClients()
    {
        return _clientRepository.GetClients().Select(ClientMappings.ToDomainModel);
    }

    public void RemoveClient(string nif)
    {
        throw new NotImplementedException();
    }

    public Client UpdateClient(Client client)
    {
        throw new NotImplementedException();
    }
}