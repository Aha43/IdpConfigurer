namespace IdpConfigurer.Domain;

public record class IdpData
{
    public string? Description { get; set; }
    public string? ServiceModel { get; set; }
    public string? ServiceVersion { get; set; }
    public int MaxNumberOfProducts { get; set; }
    public IEnumerable<string> Products { get; set; } = Enumerable.Empty<string>();
    public int MaxNumberOfClients { get; set; }
    public IdpCustomData CustomData { get; set; } = new();
}
