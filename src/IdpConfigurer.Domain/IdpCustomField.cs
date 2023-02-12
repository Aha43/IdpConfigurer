namespace IdpConfigurer.Domain
{
    public record class IdpCustomField
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string? Value { get; set; }
        public string Default { get; set; } = string.Empty;
    }
}
