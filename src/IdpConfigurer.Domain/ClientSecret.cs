namespace IdpConfigurer.Domain;

public record class ClientSecret
{
    public string Type { get; set; } = "SharedSecret";
    public string Description { get; set; } = string.Empty;
    public required string Value { get; set; }
}
