namespace IdpConfigurer.Domain.Param.Client;

public record class CreateClientParam
{
    public required string IdpName { get; init; }
    public required string ClientId { get; init; }
    public required string ClientName { get; init; }
}
