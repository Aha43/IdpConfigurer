namespace IdpConfigurer.Domain;

public record class Idp
{
    public required string Name { get; init; }
    public required string Uri { get; init; }
}
