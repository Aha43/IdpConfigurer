namespace IdpConfigurer.Domain.Param.Client;

public record class ReadClientsParam
{
    public required string IdpName { get; set; }
}
