using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;

namespace IdentityModelManager.Specification;

public interface IIdpApi
{
    Task<Idp> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken);
    Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken cancellationToken);
    Task<Idp> ReadIdpAsync(ReadIdpParam param, CancellationToken cancellationToken);
    Task<Idp> UpdateIdpAsync(Idp idp, CancellationToken cancellationToken);
    Task<bool> DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken);
}
