using System.Security.Cryptography;
using System.Text;

namespace IdpConfigurer.Util;

public static class StringHashExtensions
{
    public static string Sha256(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    public static string Sha512(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

}
