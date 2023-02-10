using IdpConfigurer.Business.ViewModel;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Specification.Api;
using IdpConfigurer.Specification.Tool;
using IdpConfigurer.Util;

namespace IdpConfigurer.Business.ViewController;

public class ClientViewController
{
    private readonly IClientApi _clientApi;
    private readonly IApiScopeApi _apiScopeApi;
    private ISharedSecretGenerator _sharedSecretGenerator;

    public string? IdpName { get; private set; }

    public Client? Client { get; private set; }

    #region Settings
    public bool AllowOfflineAccess { get; set; }
    public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
    #endregion

    #region Grant_flow
    public bool Hybrid { get; set; }
    public bool ResourceOwnerPassword { get; set; }
    public bool Implicit { get; set; }
    public bool AuthorizationCode { get; set; }
    public bool ClientCredentials { get; set; }
    #endregion

    public ApiScopeViewModel[] SelectedApiScopes { get; private set; } = Array.Empty<ApiScopeViewModel>();

    public ClientViewController(
        IClientApi clientApi,
        IApiScopeApi apiScopeApi,
        ISharedSecretGenerator sharedSecretGenerator)
    {
        _clientApi = clientApi;
        _apiScopeApi = apiScopeApi;
        _sharedSecretGenerator = sharedSecretGenerator;
    }

    #region Load
    public async Task LoadAsync(string idpName, string clientId) => await LoadAsync(idpName, clientId, default).ConfigureAwait(false);
    public async Task LoadAsync(string idpName, string clientId, CancellationToken cancellationToken)
    {
        IdpName = idpName;

        var readClientParam = new ReadClientParam { IdpName = idpName, ClientId = clientId };
        Client = await _clientApi.ReadClientAsync(readClientParam, cancellationToken).ConfigureAwait(false);

        AllowOfflineAccess = Client.AllowOfflineAccess;
        AlwaysIncludeUserClaimsInIdToken = Client.AlwaysIncludeUserClaimsInIdToken;

        await LoadApiScopes(idpName, Client, cancellationToken).ConfigureAwait(false);

        SetGrantsFlows(Client);
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

    private void SetGrantsFlows(Client client)
    {
        Hybrid = false;
        AuthorizationCode = false;
        ClientCredentials = false;

        foreach (var g in client.AllowedGrantTypes)
        {
            switch (g)
            {
                case GrantType.Hybrid: Hybrid = true; break;
                case GrantType.ResourceOwnerPassword: ResourceOwnerPassword = true; break;
                case GrantType.ClientCredentials: ClientCredentials = true; break;
                case GrantType.Implicit: Implicit = true; break;
                case GrantType.AuthorizationCode: AuthorizationCode = true; break;
            }
        }
    }
    #endregion

    #region SharedSecret

    #endregion

    #region redirectUris
    public string? RedirectUriErrorMessage = null;

    public string? NewRedirectUri { get; set; }

    public async Task AddRedirectUriAsync() => await AddRedirectUriAsync(default).ConfigureAwait(false);
    public async Task AddRedirectUriAsync(CancellationToken cancellationToken)
    {
        RedirectUriErrorMessage = null;

        if (Client == null) return;
        if (string.IsNullOrEmpty(NewRedirectUri)) return;

        NewRedirectUri = NewRedirectUri!.Trim();
        if (!Client.RedirectUris.Contains(NewRedirectUri))
        {
            if (!NewRedirectUri.ValidAbsoluteUri())
            {
                RedirectUriErrorMessage = $"Redirect URI '{NewRedirectUri}' not valid absolute URI";
                return;
            }

            Client.RedirectUris.Add(NewRedirectUri);
            await UpdateClient(cancellationToken).ConfigureAwait(false);
            NewRedirectUri = null;
        }
    }

    public async Task RemoveRedirectUriAsync(string uri) => await RemoveRedirectUriAsync(uri, default).ConfigureAwait(false);
    public async Task RemoveRedirectUriAsync(string uri, CancellationToken cancellationToken)
    {
        if (Client == null) return;
        
        Client.RedirectUris.Remove(uri);
        await UpdateClient(cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region PostLogoutRedirectUris
    public string? PostLogoutRedirectUriErrorMessage = null;

    public string? NewPostLogoutRedirectUri { get; set; }

    public async Task AddPostLogoutRedirectUriAsync() => await AddPostLogoutRedirectUriAsync(default).ConfigureAwait(false);
    public async Task AddPostLogoutRedirectUriAsync(CancellationToken cancellationToken)
    {
        PostLogoutRedirectUriErrorMessage = null;

        if (Client == null) return;
        if (string.IsNullOrEmpty(NewPostLogoutRedirectUri)) return;

        NewPostLogoutRedirectUri = NewPostLogoutRedirectUri!.Trim();
        if (!Client.PostLogoutRedirectUris.Contains(NewPostLogoutRedirectUri))
        {
            if (!NewPostLogoutRedirectUri.ValidAbsoluteUri())
            {
                PostLogoutRedirectUriErrorMessage = $"Redirect URI '{NewPostLogoutRedirectUri}' not valid absolute URI";
                return;
            }

            Client.PostLogoutRedirectUris.Add(NewPostLogoutRedirectUri);
            await UpdateClient(cancellationToken).ConfigureAwait(false);
            NewPostLogoutRedirectUri = null;
        }
    }

    public async Task RemovePostLogoutRedirectUriAsync(string uri) => await RemovePostLogoutRedirectUriAsync(uri, default).ConfigureAwait(false);
    public async Task RemovePostLogoutRedirectUriAsync(string uri, CancellationToken cancellationToken)
    {
        if (Client == null) return;

        Client.PostLogoutRedirectUris.Remove(uri);
        await UpdateClient(cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region AllowedCorsOrigins
    public string? AllowedCorsOriginsUriErrorMessage = null;

    public string? NewAllowedCorsOriginsUri { get; set; }

    public async Task AddAllowedCorsOriginsUriAsync() => await AddAllowedCorsOriginsUriAsync(default).ConfigureAwait(false);
    public async Task AddAllowedCorsOriginsUriAsync(CancellationToken cancellationToken)
    {
        AllowedCorsOriginsUriErrorMessage = null;

        if (Client == null) return;
        if (string.IsNullOrEmpty(NewAllowedCorsOriginsUri)) return;

        NewAllowedCorsOriginsUri = NewAllowedCorsOriginsUri!.Trim();
        if (!Client.AllowedCorsOrigins.Contains(NewAllowedCorsOriginsUri))
        {
            if (!NewAllowedCorsOriginsUri.ValidAbsoluteUri())
            {
                AllowedCorsOriginsUriErrorMessage = $"Redirect URI '{NewAllowedCorsOriginsUri}' not valid absolute URI";
                return;
            }

            Client.AllowedCorsOrigins.Add(NewAllowedCorsOriginsUri);
            await UpdateClient(cancellationToken).ConfigureAwait(false);
            NewAllowedCorsOriginsUri = null;
        }
    }

    public async Task RemoveAllowedCorsOriginsUriAsync(string uri) => await RemoveAllowedCorsOriginsUriAsync(uri, default).ConfigureAwait(false);
    public async Task RemoveAllowedCorsOriginsUriAsync(string uri, CancellationToken cancellationToken)
    {
        if (Client == null) return;

        Client.AllowedCorsOrigins.Remove(uri);
        await UpdateClient(cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region Save
    public async Task SaveSettings() => await SaveSettings(default);
    public async Task SaveSettings(CancellationToken cancellationToken)
    {
        if (Client == null) return;

        Client.AllowOfflineAccess = AllowOfflineAccess;
        Client.AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken;

        await UpdateClient(cancellationToken);
    }

    public async Task SaveGrantsFlows() => await SaveGrantsFlows(default);
    public async Task SaveGrantsFlows(CancellationToken cancellationToken)
    {
        if (Client == null) return;

        Client.AllowedGrantTypes.Clear();

        if (Hybrid) Client.AllowedGrantTypes.Add(GrantType.Hybrid);
        if (ResourceOwnerPassword) Client.AllowedGrantTypes.Add(GrantType.ResourceOwnerPassword);
        if (Implicit) Client.AllowedGrantTypes.Add(GrantType.Implicit);
        if (AuthorizationCode) Client.AllowedGrantTypes.Add(GrantType.AuthorizationCode);
        if (ClientCredentials) Client.AllowedGrantTypes.Add(GrantType.ClientCredentials);

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
        if (IdpName == null) return;
        if (Client == null) return;

        var param = new UpdateClientParam { IdpName = IdpName, Client = Client };
        await _clientApi.UpdateClientAsync(param, cancellationToken).ConfigureAwait(false);
    }
    #endregion

}
