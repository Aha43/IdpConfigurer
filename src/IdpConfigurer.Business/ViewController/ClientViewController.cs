using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.ApiScope;
using IdentityModelManager.Domain.Param.Client;
using IdentityModelManager.Specification;
using IdpConfigurer.Business.ViewModel;

namespace IdpConfigurer.Business.ViewController
{
    public class ClientViewController
    {
        private readonly IClientApi _clientApi;
        private readonly IApiScopeApi _apiScopeApi;

        public string? IdpName { get; private set; }

        public Client? Client { get; private set; }

        public IEnumerable<SelectedApiScope> SelectedApiScopes { get; private set; } = Enumerable.Empty<SelectedApiScope>();

        public ClientViewController(
            IClientApi clientApi,
            IApiScopeApi apiScopeApi)
        {
            _clientApi = clientApi;
            _apiScopeApi = apiScopeApi;
        }

        public async Task LoadAsync(string idpName, string clientId) => await LoadAsync(idpName, clientId, default).ConfigureAwait(false);
        public async Task LoadAsync(string idpName, string clientId, CancellationToken cancellationToken)
        {
            IdpName = idpName;

            var readClientParam = new ReadClientParam { IdpName = idpName, ClientId = clientId };
            Client = await _clientApi.ReadClientAsync(readClientParam, cancellationToken).ConfigureAwait(false);

            var readApiScopesParam = new ReadApiScopesParam { IdpName = idpName };
            var apis = await _apiScopeApi.ReadApiScopesAsync(readApiScopesParam, cancellationToken).ConfigureAwait(false);
            SelectedApiScopes = apis.Select(e => new SelectedApiScope { ApiScope = e });
        }

        #region redirectUris
        public string? NewRedirectUri { get; set; }

        public async Task AddRedirectUriAsync() => await AddRedirectUriAsync(default).ConfigureAwait(false);
        public async Task AddRedirectUriAsync(CancellationToken cancellationToken)
        {
            if (Client != null && !string.IsNullOrEmpty(NewRedirectUri))
            {
                NewRedirectUri = NewRedirectUri.Trim();
                if (!Client.RedirectUris.Contains(NewRedirectUri))
                {
                    Client.RedirectUris.Add(NewRedirectUri);
                    await UpdateClient(cancellationToken).ConfigureAwait(false);
                    NewRedirectUri = null;
                }
            }
        }

        public async Task RemoveRedirectUriAsync(string uri) => await RemoveRedirectUriAsync(uri, default).ConfigureAwait(false);
        public async Task RemoveRedirectUriAsync(string uri, CancellationToken cancellationToken)
        {
            if (Client != null && Client.RedirectUris.Contains(uri))
            {
                Client.RedirectUris.Remove(uri);
                await UpdateClient(cancellationToken).ConfigureAwait(false);
            }
        }
        #endregion

        private async Task UpdateClient(CancellationToken cancellationToken)
        {
            if (IdpName != null && Client != null)
            {
                var param = new UpdateClientParam { IdpName = IdpName, Client = Client };
                await _clientApi.UpdateClientAsync(param, cancellationToken).ConfigureAwait(false);
            }
        }

    }

}
