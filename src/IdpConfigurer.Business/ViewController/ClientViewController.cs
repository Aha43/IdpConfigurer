using IdpConfigurer.Business.ViewModel;
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
        IdpName = idpName;

        var readClientParam = new ReadClientParam { IdpName = idpName, ClientId = clientId };
        Client = await _clientApi.ReadClientAsync(readClientParam, cancellationToken).ConfigureAwait(false);

        SetSettings(Client);

        await LoadApiScopes(idpName, Client, cancellationToken).ConfigureAwait(false);

        SetGrantsFlows(Client);
    }

    private async Task UpdateClient(CancellationToken cancellationToken)
    {
        if (IdpName == null) return;
        if (Client == null) return;

        var param = new UpdateClientParam { IdpName = IdpName, Client = Client };
        await _clientApi.UpdateClientAsync(param, cancellationToken).ConfigureAwait(false);
    }

}
