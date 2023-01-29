namespace IdpAdmin.Domain.Param.Idp
{
    public record class CreateIdpParam
    {
        public required string Name { get; init; }
        public required string Uri { get; init; }
    }
}
