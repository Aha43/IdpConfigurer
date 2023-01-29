namespace IdpAdmin.Domain.Param.Idp;

public record class DeleteIdpParam
{
    public required string Name { get; init; }
}
