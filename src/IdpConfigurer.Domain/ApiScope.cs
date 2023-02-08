namespace IdpConfigurer.Domain;

public record class ApiScope
{
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
}
