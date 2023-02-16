namespace IdpConfigurer.Domain
{
    public record class IdpCustomDataDefinition
    {
        public IEnumerable<IdpCustomFieldDefinition> FieldDefinitions { get; set; } = Enumerable.Empty<IdpCustomFieldDefinition>();

        public IdpCustomData Create()
        {
            return new IdpCustomData
            {
                CustomFields = FieldDefinitions.Select(e => e.CreateField())
            };
        }

    }

}
