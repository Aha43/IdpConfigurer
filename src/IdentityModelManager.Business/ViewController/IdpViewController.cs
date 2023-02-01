using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Client;
using IdentityModelManager.Domain.Param.Idp;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Business.ViewController
{
    public class IdpViewController
    {
        private readonly IIdpApi _idpApi;
        private readonly IClientApi _clientApi;

        public Idp? Idp { get; private set; }

        private readonly List<Client> _clients = new();
        public IEnumerable<Client> Clients => _clients.AsEnumerable();

        public IdpViewController(
            IIdpApi idpApi,
            IClientApi clientApi)
        {
            _idpApi = idpApi;
            _clientApi = clientApi;
        }

        public async Task LoadAsync(string name) => await LoadAsync(name, default).ConfigureAwait(false);
        public async Task LoadAsync(string name, CancellationToken cancellationToken = default)
        {
            var readIdParam = new ReadIdpParam { Name = name };
            Idp = await _idpApi.ReadIdpAsync(readIdParam, cancellationToken).ConfigureAwait(false);

            _clients.Clear();
            var readClientsParam = new ReadClientsParam { IdpName = name };
            var clients = await _clientApi.ReadClientsAsync(readClientsParam, cancellationToken).ConfigureAwait(false);
            _clients.AddRange(clients);
        }

        public string? NewClientName { get; set; }
        public string? NewClientId { get; set; }

        public async Task CreateClientAsync() => await CreateClientAsync(default);
        public async Task CreateClientAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(NewClientId) && !string.IsNullOrEmpty(NewClientName) && Idp != null) 
            { 
                var param = new CreateClientParam { ClientName = NewClientName, ClientId = NewClientId, IdpName = Idp.Name };
                var client = await _clientApi.CreateClientAsync(param, cancellationToken).ConfigureAwait(false);
                _clients.Add(client);
            }
        }

    }

}
