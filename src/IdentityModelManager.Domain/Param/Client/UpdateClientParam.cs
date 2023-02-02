using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModelManager.Domain.Param.Client
{
    public record class UpdateClientParam
    {
        public required string IdpName { get; init; }
        public required IdentityModelManager.Domain.Client Client { get; init; }
    }
}
