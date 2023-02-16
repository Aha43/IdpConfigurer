using IdpConfigurer.Domain.Param.Client;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
        public bool Deleted { get; private set; } = false;

        public async Task DeleteAsync() => await DeleteAsync(default);
        public async Task DeleteAsync(CancellationToken cancellationToken)
        {
            if (Client == null || IdpName == null) return;

            var deleteParam = new DeleteClientParam { IdpName = IdpName, ClientId = Client.ClientId };
            await _clientApi.DeleteClientAsync(deleteParam, cancellationToken).ConfigureAwait(false);

            Deleted = true;
        }

    }

}
