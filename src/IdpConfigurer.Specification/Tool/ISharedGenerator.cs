namespace IdpConfigurer.Specification.Tool;

public interface ISharedGenerator
{
    (string hash, string? err) GenerateFromGivenPlainText(string plainTextSecret);
    (string hash, string plainTextSecret, string? err) Generate(int length = 40);
}
