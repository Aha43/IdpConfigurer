using IdpConfigurer.Util;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
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

    }

}
