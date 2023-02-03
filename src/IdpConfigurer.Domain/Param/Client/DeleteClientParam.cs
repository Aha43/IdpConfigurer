namespace IdpConfigurer.Domain.Param.Client;

public record class DeleteClientParam
{
    public required string IdpName { get; init; }
    public required string ClientId { get; init; }
}
