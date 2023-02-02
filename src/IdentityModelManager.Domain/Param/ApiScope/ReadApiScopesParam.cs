namespace IdentityModelManager.Domain.Param.ApiScope;

public record class ReadApiScopesParam
{
    public required string IdpName { get; init; }
}
