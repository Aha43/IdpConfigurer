using IdpConfigurer.Business.ViewModel;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
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

    }

}
