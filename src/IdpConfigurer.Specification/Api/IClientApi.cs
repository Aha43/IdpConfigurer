using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;

namespace IdpConfigurer.Specification.Api;

public interface IClientApi
{
    Task<Client> CreateClientAsync(CreateClientParam param, CancellationToken cancellationToken);
    Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam param, CancellationToken cancellationToken);
    Task<IEnumerable<Client>> ReadClientAsync(ReadClientParam param, CancellationToken cancellationToken);
    Task<Client> UpdateClientAsync(UpdateClientParam param, CancellationToken cancellationToken);
    Task DeleteClientAsync(DeleteClientParam param, CancellationToken cancellationToken);
}
