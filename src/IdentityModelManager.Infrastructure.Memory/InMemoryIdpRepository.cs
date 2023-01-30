using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Idp;
using IdentityModelManager.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModelManager.Infrastructure.Memory
{
    public class InMemoryIdpRepository : IIdpApi
    {
        private readonly Dictionary<string, Idp> _idps = new();

        public Task<IEnumerable<Idp>> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken)
        {
            if (_idps.ContainsKey(param.Name)) throw new ArgumentException($"Idp named '{param.Name}' exists");

            var retVal = new Idp { Name = param.Name, Uri = param.Uri };
            _idps[param.Name] = retVal;
            return Task.FromResult(_idps.Values.AsEnumerable());
        }

        public Task<bool> DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken)
        {
            if (!_idps.ContainsKey(param.Name)) return Task.FromResult(false);

            _idps.Remove(param.Name);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken cancellationToken) => Task.FromResult(_idps.Values.AsEnumerable());

        public Task<Idp> UpdateIdpAsync(Idp idp, CancellationToken cancellationToken)
        {
            if (!_idps.ContainsKey(idp.Name)) throw new ArgumentException($"Idp named '{idp.Name}' does not exists");

            _idps[idp.Name] = idp;
            return Task.FromResult(idp);
        }

    }

}
