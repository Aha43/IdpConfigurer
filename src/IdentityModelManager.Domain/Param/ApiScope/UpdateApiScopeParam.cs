namespace IdentityModelManager.Domain.Param.ApiScope;

public record class UpdateApiScopeParam
{
    public required string IdpName { get; init; }
    public required IdentityModelManager.Domain.ApiScope ApiScope { get; init; }
}
