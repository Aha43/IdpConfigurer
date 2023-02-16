namespace IdpConfigurer.Domain;

public record class IdpData
{
    public IdpCustomData CustomData { get; set; } = new();
}
