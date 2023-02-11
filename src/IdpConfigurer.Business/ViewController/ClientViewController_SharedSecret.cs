using IdpConfigurer.Domain;

namespace IdpConfigurer.Business.ViewController;

public partial class ClientViewController
{
    public string? NewSharedSecretPlainText { get; set; }
    public string? NewSharedSecretDescription { get; set; }

    public string? SharedSecretErrorMessage { get; private set; }

    public void GenerateNewSharedSecretPlainText()// => NewSharedSecretPlainText = _sharedSecretGenerator.GeneratePlainTextSecret();
    {
        NewSharedSecretPlainText = _sharedSecretGenerator.GeneratePlainTextSecret();
        SharedSecretErrorMessage = null;
    }

    public async Task SaveSharedSecretAsync() => await SaveSharedSecretAsync(default).ConfigureAwait(false);
    public async Task SaveSharedSecretAsync(CancellationToken cancellationToken)
    {
        var plain = NewSharedSecretPlainText;
        var desc = NewSharedSecretDescription;

        NewSharedSecretPlainText = null;
        NewSharedSecretDescription = null;

        if (Client == null) return;

        if (string.IsNullOrWhiteSpace(plain)) return;

        var (hash, err) = _sharedSecretGenerator.GenerateFromGivenPlainText(plain);

        if (Client.ClientSecrets.Where(e => e.Type.Equals(ClientSecretTypes.SharedSecret) && e.Value.Equals(hash)).Any())
        {
            SharedSecretErrorMessage = "Duplicate shared secret";
            return;
        }

        var sharedSecret = new ClientSecret { Type = ClientSecretTypes.SharedSecret, Description = desc, Value = hash };

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
