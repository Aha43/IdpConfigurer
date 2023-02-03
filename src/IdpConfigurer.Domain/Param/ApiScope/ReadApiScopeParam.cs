namespace IdpConfigurer.Domain.Param.ApiScope;

public record class ReadApiScopeParam
{
    public required string IdpName { get; init; }
    public required string Name { get; init; }
}
