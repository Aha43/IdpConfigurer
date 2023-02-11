namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
        public bool AllowOfflineAccess { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        public async Task SaveSettings() => await SaveSettings(default);
        public async Task SaveSettings(CancellationToken cancellationToken)
        {
            if (Client == null) return;

            Client.AllowOfflineAccess = AllowOfflineAccess;
            Client.AlwaysIncludeUserClaimsInIdToken = AlwaysIncludeUserClaimsInIdToken;

            await UpdateClient(cancellationToken);
        }
    }
}
