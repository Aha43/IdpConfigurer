namespace IdpConfigurer.Domain.Param.Idp;

public record class UpdateIdpParam
{
    public string? IdpName { get; init; } = null;
    public required Domain.Idp Idp { get; init; }
}
