using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Business.ViewController
{
    public class ApiScopeViewController
    {
        private readonly IApiScopeApi _apiScopeApi;
        private readonly IClientApi _clientApi;

        public bool Deleted { get; private set; } = false;

        public string? IdpName { get; private set; }

        public ApiScope? ApiScope { get; private set; }

        public IEnumerable<Client> Clients { get; private set; } = Enumerable.Empty<Client>();

        public bool Deletable => !Clients.Any();

        public ApiScopeViewController(
            IApiScopeApi apiScopeApi,
            IClientApi clientApi)
        {
            _apiScopeApi = apiScopeApi;
            _clientApi = clientApi;
        }

        public async Task LoadAsync(string idpName, string apiName) => await LoadAsync(idpName, apiName, default).ConfigureAwait(false);
        public async Task LoadAsync(string idpName, string apiName, CancellationToken cancellationToken)
        {
            IdpName = idpName;

            var readApiScopeParam = new ReadApiScopeParam { IdpName = idpName, Name = apiName };
            ApiScope = await _apiScopeApi.ReadApiScopeAsync(readApiScopeParam, cancellationToken).ConfigureAwait(false);

            var readClientsParam = new ReadClientsParam { IdpName = idpName };
            var clients = await _clientApi.ReadClientsAsync(readClientsParam, cancellationToken).ConfigureAwait(false);
            Clients = clients.Where(e => e.AllowedScopes.Contains(apiName)).ToList();
        }

        public async Task DeleteAsync() => await DeleteAsync(default).ConfigureAwait(false);
        public async Task DeleteAsync(CancellationToken cancellationToken)
        {
            if (ApiScope == null || IdpName == null) return;
            if (!Deletable) return;

            var param = new DeleteApiScopeParam { IdpName = IdpName, Name = ApiScope.Name };
            await _apiScopeApi.DeleteApiScopeAsync(param, cancellationToken).ConfigureAwait(false);
            Deleted = true; 
        }

    }

}
