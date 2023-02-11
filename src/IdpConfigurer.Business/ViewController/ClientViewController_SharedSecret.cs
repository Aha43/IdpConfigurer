using IdpConfigurer.Domain;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
        public string? NewSharedSecretPlainText;
        public string? NewSharedSecretDescription;

        public void GenerateNewSharedSecretPlainText() => NewSharedSecretPlainText = _sharedSecretGenerator.GeneratePlainTextSecret();

        public async Task SaveSharedSecretAsync() => await SaveSharedSecretAsync(default).ConfigureAwait(false);
        public async Task SaveSharedSecretAsync(CancellationToken cancellationToken)
        {
            var plain = NewSharedSecretPlainText;
            var desc = NewSharedSecretDescription;

            NewSharedSecretPlainText = null;
            NewSharedSecretDescription = null;

            if (Client == null) return;

            if (string.IsNullOrWhiteSpace(plain)) return;

            var result = _sharedSecretGenerator.GenerateFromGivenPlainText(plain);

            var sharedSecret = new ClientSecret { Type = ClientSecretTypes.SharedSecret, Description = desc, Value = result.hash };

            Client.ClientSecrets.Add(sharedSecret);

            await UpdateClient(cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveSharedSecretAsync(ClientSecret secret) => await RemoveSharedSecretAsync(secret, default).ConfigureAwait(false);
        public async Task RemoveSharedSecretAsync(ClientSecret secret, CancellationToken cancellation)
        {
            if (Client == null) return;

            if (secret.Type != ClientSecretTypes.SharedSecret) return;

            if (Client.ClientSecrets.Remove(secret))
            {
                await UpdateClient(cancellation).ConfigureAwait(false);
            }
        }

    }

}
