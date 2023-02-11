using IdpConfigurer.Util;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
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

    }

}
