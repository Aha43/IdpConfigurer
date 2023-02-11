using IdpConfigurer.Util;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
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

    }

}
