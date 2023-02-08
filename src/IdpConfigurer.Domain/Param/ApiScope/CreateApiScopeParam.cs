namespace IdpConfigurer.Domain.Param.ApiScope;

public class CreateApiScopeParam
{
    public required string IdpName { get; init; }
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
}
