namespace IdpAdmin.Domain.Param
{
    public record class CreateIdpParam
    {
        public required string Name { get; init; }
        public required string Uri { get; init; }
    }
}
