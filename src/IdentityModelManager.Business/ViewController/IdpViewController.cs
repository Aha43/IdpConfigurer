using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.ApiScope;
using IdentityModelManager.Domain.Param.Client;
using IdentityModelManager.Domain.Param.Idp;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Business.ViewController
{
    public class IdpViewController
    {
        private readonly IIdpApi _idpApi;
        private readonly IClientApi _clientApi;
        private readonly IApiScopeApi _apiScopeApi;

        public Idp? Idp { get; private set; }

        private readonly List<Client> _clients = new();
        public IEnumerable<Client> Clients => _clients.AsEnumerable();

        private readonly List<ApiScope> _apiScopes = new();
        public IEnumerable<ApiScope> ApiScopes => _apiScopes.AsEnumerable();

        public IdpViewController(
            IIdpApi idpApi,
            IClientApi clientApi,
            IApiScopeApi apiScopeApi)
        {
            _idpApi = idpApi;
            _clientApi = clientApi;
            _apiScopeApi = apiScopeApi;
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

        public async Task CreateClientAsync() => await CreateClientAsync(default).ConfigureAwait(true);
        public async Task CreateClientAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(NewClientId) && !string.IsNullOrEmpty(NewClientName) && Idp != null) 
            { 
                var param = new CreateClientParam { ClientName = NewClientName, ClientId = NewClientId, IdpName = Idp.Name };
                var client = await _clientApi.CreateClientAsync(param, cancellationToken).ConfigureAwait(true);
                _clients.Add(client);
            }

            NewClientId = null;
            NewClientName = null;
        }

        public string? NewApiScopeName { get; set; }
        public string? NewApiScopeDisplayName { get; set; }

        public async Task CreateApiScope() => await CreateApiScope(default).ConfigureAwait(true);
        public async Task CreateApiScope(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(NewApiScopeName) && Idp != null)
            {
                var param = new CreateApiScopeParam { Name = NewApiScopeName, DisplayName = NewApiScopeDisplayName, IdpName = Idp.Name };
                var scope = await _apiScopeApi.CreateApiScopeAsync(param, cancellationToken).ConfigureAwait(true);
                _apiScopes.Add(scope);
            }

            NewApiScopeName = null;
            NewApiScopeDisplayName = null;
        }

    }

}
