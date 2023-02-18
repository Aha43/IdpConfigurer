namespace IdpConfigurer.Domain
{
    public record class IdpCustomData
    {
        public IEnumerable<IdpCustomField> CustomFields { get; set; } = Enumerable.Empty<IdpCustomField>();

        public bool ContainsFieldDefinedBy(IdpCustomFieldDefinition def) => 
            CustomFields.Where(e => e.DefinedBy(def)).Any();

        public void Update(IdpCustomDataDefinition definition)
        {
            var l = new List<IdpCustomField>();

            // Find the ones to keep
            foreach (var field in CustomFields) 
            {
                if (definition.IfDefinesThenUpdate(field))
                {
                    l.Add(field);
                }
            }

            // Find new ones to add
            foreach (var def in definition.FieldDefinitions) 
            {
                if (!ContainsFieldDefinedBy(def)) 
                {
                    l.Add(def.CreateField());
                }
            }
        }

    }

}
