using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification;
using System.Runtime.Intrinsics.Arm;

namespace IdpConfigurer.Infrastructure.Memory
{
    public class InMemoryIdpRepository : IIdpApi
    {
        private readonly Dictionary<string, Idp> _idps = new();

        public InMemoryIdpRepository()
        {
            AddIdp(new Idp { Name = "TestIdp1", Uri = "https://TestIdp1" }).
            AddIdp(new Idp { Name = "TestIdp2", Uri = "https://TestIdp2" });
        }

        public Task<Idp> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken)
        {
            var retVal = new Idp { Name = param.Name, Uri = param.Uri };
            AddIdp(retVal);
            return Task.FromResult(retVal);
        }

        private InMemoryIdpRepository AddIdp(Idp idp)
        {
            if (_idps.ContainsKey(idp.Name)) throw new ArgumentException($"Idp named '{idp.Name}' exists");
            _idps[idp.Name] = idp;
            return this;
        }

        public Task DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken)
        {
            if (!_idps.ContainsKey(param.Name)) return Task.CompletedTask;

            _idps.Remove(param.Name);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken cancellationToken) => Task.FromResult(_idps.Values.AsEnumerable());

        public Task<Idp> ReadIdpAsync(ReadIdpParam param, CancellationToken cancellationToken)
        {
            if (_idps.TryGetValue(param.Name, out Idp? idp)) return Task.FromResult(idp);
            throw new ArgumentException($"No Idp named '{param.Name}'");
        }

        public Task<Idp> UpdateIdpAsync(UpdateIdpParam param, CancellationToken cancellationToken)
        {
            if (!_idps.ContainsKey(param.Idp.Name)) throw new ArgumentException($"Idp named '{param.Idp.Name}' does not exists");

            if (param.IdpName == null)
            {
                _idps[param.Idp.Name] = param.Idp;
                return Task.FromResult(param.Idp);
            }
            else
            {
                var newIdp = param.Idp with { Name = param.IdpName };
                _idps.Remove(param.Idp.Name);
                _idps[newIdp.Name] = newIdp;
                return Task.FromResult(newIdp);
            }
        }

    }

}
