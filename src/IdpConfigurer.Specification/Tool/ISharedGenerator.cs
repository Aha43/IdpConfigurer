namespace IdpConfigurer.Specification.Tool;

public interface ISharedSecretGenerator
{
    string GeneratePlainTextSecret(int length = 40);
    (string hash, string? err) GenerateFromGivenPlainText(string plainTextSecret);
    (string hash, string plainTextSecret, string? err) Generate(int length = 40);
}
