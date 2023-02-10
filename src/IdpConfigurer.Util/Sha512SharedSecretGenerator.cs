namespace IdpConfigurer.Util;

public sealed class Sha512SharedSecretGenerator : SharedGeneratorBase
{
    protected override string Hash(string hash) => hash.Sha512();
}
