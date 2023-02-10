using IdpConfigurer.Specification.Tool;
using System.Security.Cryptography;

namespace IdpConfigurer.Util;

public abstract class SharedGeneratorBase : ISharedSecretGenerator
{
    public (string hash, string plainTextSecret, string? err) Generate(int length = 40)
    {
        var plainText = GeneratePlainTextSecret(length);
        var (hash, err) = GenerateFromGivenPlainText(plainText);
        return (hash, plainText, err);
    }

    public (string hash, string? err) GenerateFromGivenPlainText(string plainTextSecret)
    {
        var trimmed = plainTextSecret.Trim();
        if (trimmed.Length == 0) 
        {
            return (string.Empty, "Secret can not be or degenerate to the empty string");
        }
        if (trimmed.Length < 8) 
        {
            return (string.Empty, "Password length < 8");
        }

        var hash = Hash(plainTextSecret);
        return (hash, null);
    }

    public string GeneratePlainTextSecret(int length = 40)
    {
        var bytes = RandomNumberGenerator.GetBytes(length);
        var plainText = Convert.ToBase64String(bytes);
        return plainText;
    }

    protected abstract string Hash(string hash);

}
