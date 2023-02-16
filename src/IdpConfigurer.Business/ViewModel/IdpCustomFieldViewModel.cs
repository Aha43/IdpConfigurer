using IdpConfigurer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdpConfigurer.Business.ViewModel
{
    public class IdpCustomFieldViewModel
    {
        public IdpCustomField Model { get; private set; }

        public IdpCustomFieldViewModel(IdpCustomField model) => Model = model;
        
        public bool BoolValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Model.Value)) return false;
                return bool.Parse(Model.Value);
            }

            set => Model.Value = value.ToString();
        }
    }
}
