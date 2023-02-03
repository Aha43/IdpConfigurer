namespace IdpConfigurer.Domain.Param.ApiScope;

public record class UpdateApiScopeParam
{
    public required string IdpName { get; init; }
    public required Domain.ApiScope ApiScope { get; init; }
}
