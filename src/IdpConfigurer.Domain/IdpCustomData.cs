namespace IdpConfigurer.Domain
{
    public record class IdpCustomData
    {
        public IEnumerable<IdpCustomField> CustomFields { get; set; } = Enumerable.Empty<IdpCustomField>();

        public bool ContainsFieldDefinedBy(IdpCustomFieldDefinition def) => 
            CustomFields.Where(e => e.DefinedBy(def)).Any();

        public bool Update(IdpCustomDataDefinition definition)
        {
            var l = new List<IdpCustomField>();

            // Find the ones to keep and update if def changed

            bool fieldUpdated = false;

            foreach (var field in CustomFields) 
            {
                var (defines, updated) = definition.IfDefinesThenUpdate(field);
                if (defines) l.Add(field);
                if (updated) fieldUpdated = true;
            }

            // Find new ones to add

            bool fieldAdded = false;
            foreach (var def in definition.FieldDefinitions) 
            {
                if (!ContainsFieldDefinedBy(def)) 
                {
                    l.Add(def.CreateField());
                    fieldAdded = true;
                }
            }

            CustomFields = l.AsEnumerable();

            var retVal = fieldUpdated || fieldAdded;

            return retVal;
        }

    }

}
