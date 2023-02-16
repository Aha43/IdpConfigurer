namespace IdpConfigurer.Domain
{
    public record class IdpCustomData
    {
        public IEnumerable<IdpCustomField> CustomFields { get; set; } = Enumerable.Empty<IdpCustomField>();
    }
}
