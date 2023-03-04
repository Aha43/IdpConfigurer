using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;

namespace IdpConfigurer.Specification.Api;

public interface IClientApi
{
    Task<Client> CreateClientAsync(CreateClientParam p, CancellationToken ct);
    Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam p, CancellationToken ct);
    Task<IEnumerable<Client>> ReadClientAsync(ReadClientParam p, CancellationToken ct);
    Task<Client> UpdateClientAsync(UpdateClientParam p, CancellationToken ct);
    Task DeleteClientAsync(DeleteClientParam p, CancellationToken ct);
}
