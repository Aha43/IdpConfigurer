using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Client;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Business.ViewController
{
    public class ClientViewController
    {
        private readonly IClientApi _clientApi;

        public string? IdpName { get; private set; }

        public Client? Client { get; private set; }

        public ClientViewController(IClientApi clientApi) => _clientApi = clientApi;

        public async Task LoadAsync(string idpName, string clientId) => await LoadAsync(idpName, clientId, default).ConfigureAwait(false);
        public async Task LoadAsync(string idpName, string clientId, CancellationToken cancellationToken)
        {
            IdpName = idpName;
            var param = new ReadClientParam { IdpName = idpName, ClientId = clientId };
            Client = await _clientApi.ReadClientAsync(param, cancellationToken).ConfigureAwait(false);
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
