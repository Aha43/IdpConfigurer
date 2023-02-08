using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification;

namespace IdpConfigurer.Infrastructure.Db
{
    public class IdpRepository : IIdpApi
    {
        public Task<Idp> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Idp> ReadIdpAsync(ReadIdpParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Idp> UpdateIdpAsync(Idp idp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
