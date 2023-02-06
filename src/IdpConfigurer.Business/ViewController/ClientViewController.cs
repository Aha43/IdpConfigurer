using IdpConfigurer.Business.ViewModel;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Specification;

namespace IdpConfigurer.Business.ViewController
{
    public class ClientViewController
    {
        private readonly IClientApi _clientApi;
        private readonly IApiScopeApi _apiScopeApi;

        public bool AllowOfflineAccess { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        public string? IdpName { get; private set; }

        public Client? Client { get; private set; }

        public ApiScopeViewModel[] SelectedApiScopes { get; private set; } = Array.Empty<ApiScopeViewModel>();

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

            AllowOfflineAccess = Client.AllowOfflineAccess;
            AlwaysIncludeUserClaimsInIdToken = Client.AlwaysIncludeUserClaimsInIdToken;

            await LoadApiScopes(idpName, Client, cancellationToken).ConfigureAwait(false);
        }

        private async Task LoadApiScopes(string idpName, Client client, CancellationToken cancellationToken)
        {
            var readApiScopesParam = new ReadApiScopesParam { IdpName = idpName };
            var apis = await _apiScopeApi.ReadApiScopesAsync(readApiScopesParam, cancellationToken).ConfigureAwait(false);
            SelectedApiScopes = apis.Select(e => new ApiScopeViewModel
            {
                ApiScope = e,
                Selected = client.AllowedScopes.Contains(e.Name)
            }).ToArray();
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

        public async Task SaveSettings() => await SaveSettings(default);
        public async Task SaveSettings(CancellationToken cancellationToken)
        {
            if (Client == null) return;

            Client.AllowOfflineAccess = AllowOfflineAccess;
            Client.AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken;

            await UpdateClient(cancellationToken);
        }

        public async Task SaveApiSelection() => await SaveApiSelection(default);
        public async Task SaveApiSelection(CancellationToken cancellationToken)
        {
            if (Client == null) return;

            Client.AllowedScopes.Clear();
            foreach (var api in SelectedApiScopes)
            {
                if (api.Selected)
                {
                    Client.AllowedScopes.Add(api.ApiScope.Name);
                }
                else
                {
                    Client.AllowedScopes.Remove(api.ApiScope.Name);
                }
            }

            await UpdateClient(cancellationToken).ConfigureAwait(false);
        }

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
