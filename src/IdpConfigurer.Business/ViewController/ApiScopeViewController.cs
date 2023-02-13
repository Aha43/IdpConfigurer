using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Business.ViewController
{
    public class ApiScopeViewController
    {
        private readonly IApiScopeApi _apiScopeApi;

        public string? IdpName { get; private set; }

        public ApiScope? ApiScope { get; private set; }

        public ApiScopeViewController(IApiScopeApi apiScopeApi) => _apiScopeApi = apiScopeApi;

        public async Task LoadAsync(string idpName, string apiName) => await LoadAsync(idpName, apiName, default).ConfigureAwait(false);
        public async Task LoadAsync(string idpName, string apiName, CancellationToken cancellationToken)
        {
            IdpName = idpName;

            var readApiScopeParam = new ReadApiScopeParam { IdpName = idpName, Name = apiName };
            ApiScope = await _apiScopeApi.ReadApiScopeAsync(readApiScopeParam, cancellationToken).ConfigureAwait(false);
        }
    }
}
