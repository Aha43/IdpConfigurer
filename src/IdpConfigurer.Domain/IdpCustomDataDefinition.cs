using System.Text;

namespace IdpConfigurer.Domain
{
    public record class IdpCustomDataDefinition
    {
        private List<IdpCustomFieldDefinition> _fields = new();
        public IEnumerable<IdpCustomFieldDefinition> FieldDefinitions 
        {
            get => _fields.AsEnumerable(); 
            set
            {
                Evaluate(value);
                _fields.Clear();
                _fields.AddRange(value);
            }
        } 

        public IdpCustomData Create()
        {
            return new IdpCustomData
            {
                CustomFields = FieldDefinitions.Select(e => e.CreateField())
            };
        }

        public bool IfDefinesThenUpdate(IdpCustomField field)
        {
            foreach (var fieldDef in FieldDefinitions) 
            {
                if (fieldDef.IfDefinesThenUpdate(field)) return true;
            }

            return false;
        }

        private static void Evaluate(IEnumerable<IdpCustomFieldDefinition> fields)
        {
            var sb = new StringBuilder();
            var set = new HashSet<string>();
            foreach (var field in fields) 
            { 
                if (set.Contains(field.Name)) sb.Append(field.Name).Append(' ');
                set.Add(field.Name);
            }

            if (sb.Length > 0) 
            {
                throw new ArgumentException($"Duplicate Idp custom field names: {sb}");
            }
        } 

    }

}
