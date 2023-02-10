namespace IdpConfigurer.Util;

public sealed class Sha256SharedSecretGenerator : SharedGeneratorBase
{
    protected override string Hash(string hash) => hash.Sha256();
}
