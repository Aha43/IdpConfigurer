using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Idp;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Infrastructure.Memory
{
    public class InMemoryIdpRepository : IIdpApi
    {
        private readonly Dictionary<string, Idp> _idps = new();

        public InMemoryIdpRepository() 
        {
            AddIdp(new Idp { Name = "TestIdp1", Uri = "https://TestIdp1" }).
            AddIdp(new Idp { Name = "TestIdp2", Uri = "https://TestIdp2" });
        }

        public Task<IEnumerable<Idp>> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken)
        {
            var retVal = new Idp { Name = param.Name, Uri = param.Uri };
            AddIdp(retVal);
            return Task.FromResult(_idps.Values.AsEnumerable());
        }

        private InMemoryIdpRepository AddIdp(Idp idp) 
        {
            if (_idps.ContainsKey(idp.Name)) throw new ArgumentException($"Idp named '{idp.Name}' exists");
            _idps[idp.Name] = idp;
            return this;
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
