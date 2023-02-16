namespace IdpConfigurer.Domain
{
    public record class IdpCustomFieldDefinition
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string Default { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public IdpCustomField CreateField()
        {
            return new IdpCustomField
            {
                Name = Name,
                Type = Type,
                Value = Default,
                Default = Default,
                Description = Description
            };
        }

    }

}
