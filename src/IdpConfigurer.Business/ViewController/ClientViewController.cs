﻿using IdpConfigurer.Business.ViewModel;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Specification.Api;
using IdpConfigurer.Specification.Tool;

namespace IdpConfigurer.Business.ViewController;

public partial class ClientViewController
{
    private readonly IClientApi _clientApi;
    private readonly IApiScopeApi _apiScopeApi;
    private ISharedSecretGenerator _sharedSecretGenerator;

    public string? IdpName { get; private set; }

    public Client? Client { get; private set; }

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

    public async Task LoadAsync(string idpName, string clientId) => await LoadAsync(idpName, clientId, default).ConfigureAwait(false);
    public async Task LoadAsync(string idpName, string clientId, CancellationToken cancellationToken)
    {
        Client = null;

        IdpName = idpName;

        var readClientParam = new ReadClientParam { IdpName = idpName, ClientId = clientId };
        var clients = await _clientApi.ReadClientAsync(readClientParam, cancellationToken).ConfigureAwait(false);
        var client = clients.FirstOrDefault();

        if (client != null)
        {
            SetSettings(client);

            await LoadApiScopes(idpName, client, cancellationToken).ConfigureAwait(false);

            SetGrantsFlows(client);

            Client = client;
        }
    }

    private async Task UpdateClient() => await UpdateClient(default).ConfigureAwait(false);

    private async Task UpdateClient(CancellationToken cancellationToken)
    {
        if (IdpName == null) return;
        if (Client == null) return;

        var param = new UpdateClientParam { IdpName = IdpName, Client = Client };
        await _clientApi.UpdateClientAsync(param, cancellationToken).ConfigureAwait(false);
    }

}
