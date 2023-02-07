using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdpConfigurer.Util
{
    public static class UriExtensions
    {
        public static bool ValidAbsoluteUri(this string uri)
        {
            if (Uri.TryCreate(uri, UriKind.Absolute, out var result)) { return true; }
            return false;
        }
    }
}
