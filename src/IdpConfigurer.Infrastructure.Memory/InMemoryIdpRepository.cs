using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification;

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

        public Task<Idp> UpdateIdpAsync(Idp idp, CancellationToken cancellationToken)
        {
            if (!_idps.ContainsKey(idp.Name)) throw new ArgumentException($"Idp named '{idp.Name}' does not exists");

            _idps[idp.Name] = idp;
            return Task.FromResult(idp);
        }

    }

}
