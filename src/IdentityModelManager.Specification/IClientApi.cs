using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Client;

namespace IdentityModelManager.Specification;

public interface IClientApi
{
    Task<Client> CreateClientAsync(CreateClientParam param, CancellationToken cancellationToken);
    Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam param, CancellationToken cancellationToken);
    Task<Client> ReadClientAsync(ReadClientParam param, CancellationToken cancellationToken);
    Task<Client> UpdateClientAsync(UpdateClientParam param, CancellationToken cancellationToken);
    Task<bool> DeleteClientAsync(DeleteClientParam param, CancellationToken cancellationToken);
}
