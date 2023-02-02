namespace IdentityModelManager.Domain.Param.ApiScope;

public class CreateApiScopeParam
{
    public required string IdpName { get; init; }
    public required string Name { get; init; }
    public string? DisplayName { get; init; }
}
