using IdentityModelManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModelManager.Business;

public static class DtoViewExtensions
{
    public static string PageUri(this Idp idp) => $"/Idp/{idp.Name}";
}
