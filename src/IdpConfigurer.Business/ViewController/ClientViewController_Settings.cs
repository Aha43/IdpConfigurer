using IdpConfigurer.Domain;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
        public bool RequirePkce { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        private void SetSettings(Client client)
        {
            RequirePkce = client.RequirePkce;
            AllowOfflineAccess = client.AllowOfflineAccess;
            AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken;
        }

        public async Task SaveSettings() => await SaveSettings(default);
        public async Task SaveSettings(CancellationToken cancellationToken)
        {
            if (Client == null) return;

            Client.RequirePkce = RequirePkce;
            Client.AllowOfflineAccess = AllowOfflineAccess;
            Client.AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken;

            await UpdateClient(cancellationToken);
        }
    }
}
