namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
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
