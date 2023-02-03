﻿using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Specification;

namespace IdpConfigurer.Infrastructure.Memory
{
    public class InMemoryClientRepository : IClientApi
    {
        private readonly Dictionary<ClientKey, Client> _clients = new();

        public Task<Client> CreateClientAsync(CreateClientParam param, CancellationToken cancellationToken)
        {
            var key = new ClientKey(param);
            if (_clients.ContainsKey(key))
            {
                throw new ArgumentException($"Client exists for key: '{key}'");
            }

            _clients[key] = new Client { ClientId = param.ClientId, ClientName = param.ClientName };
            return Task.FromResult(_clients[key]);
        }

        public Task<bool> DeleteClientAsync(DeleteClientParam param, CancellationToken cancellationToken)
        {
            var key = new ClientKey(param);
            if (_clients.ContainsKey(key))
            {
                _clients.Remove(key);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<Client> ReadClientAsync(ReadClientParam param, CancellationToken cancellationToken)
        {
            var key = new ClientKey(param);
            if (_clients.TryGetValue(key, out Client? client))
            {
                return Task.FromResult(client);
            }

            throw new ArgumentException($"Client does not exist for key: '{key}'");
        }

        public Task<IEnumerable<Client>> ReadClientsAsync(ReadClientsParam param, CancellationToken cancellationToken)
        {
            var retValues = _clients.Where(e => e.Key.IdpName.Equals(param.IdpName)).Select(e => e.Value);
            return Task.FromResult(retValues);
        }

        public Task<Client> UpdateClientAsync(UpdateClientParam param, CancellationToken cancellationToken)
        {
            var key = new ClientKey(param.IdpName, param.Client.ClientId!);
            if (_clients.ContainsKey(key))
            {
                _clients[key] = param.Client;
                return Task.FromResult(param.Client);
            }

            throw new ArgumentException($"Client does not exist for key: '{key}'");
        }

    }

    internal record class ClientKey
    {
        public string IdpName { get; private set; }
        public string ClientId { get; private set; }

        public ClientKey(string idpName, string clientId)
        {
            IdpName = idpName;
            ClientId = clientId;
        }

        public ClientKey(dynamic o)
        {
            IdpName = o.IdpName;
            ClientId = o.ClientId;
        }

    }

}