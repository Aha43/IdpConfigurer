namespace IdpConfigurer.Domain
{
    public record class IdpCustomFieldDefinition
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string Default { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

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

        public (bool defines, bool updated) IfDefinesThenUpdate(IdpCustomField field) 
        { 
            if (Name.Equals(field.Name) && Type.Equals(field.Type))
            {
                bool updated = false;
                if (!field.Description.Equals(Description))
                {
                    field.Description = Description;
                    updated = true;
                }
                if (!field.Default.Equals(Default))
                {
                    field.Description = Default;
                    updated = true;
                }
                
                return (true, updated);
            }

            return (false, false);
        }

    }

}
