namespace IdentityModelManager.Domain.Param.Client;

public record class ReadClientParam
{
    public required string IdpName { get; init; }
    public required string ClientId { get; init; }
}
