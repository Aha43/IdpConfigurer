using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Specification;

namespace IdpConfigurer.Infrastructure.Db
{
    public class ClientRepository : IClientApi
    {
        public Task<Client> CreateClientAsync(CreateClientParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteClientAsync(DeleteClientParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Client> ReadClientAsync(ReadClientParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateClientAsync(UpdateClientParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
