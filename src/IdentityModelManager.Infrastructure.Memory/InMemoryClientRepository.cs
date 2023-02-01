using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Client;
using IdentityModelManager.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModelManager.Infrastructure.Memory
{
    public class InMemoryClientRepository : IClientApi
    {
        public Task<Client> CreateClientAsync(CreateClientParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClientAsync(DeleteClientParam param, CancellationToken cancellationToken)
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

        public Task<Client> UpdateClientAsync(Client client, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
