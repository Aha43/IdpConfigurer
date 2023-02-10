using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;

namespace IdpConfigurer.Specification.Api;

public interface IIdpApi
{
    Task<Idp> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken);
    Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken cancellationToken);
    Task<Idp> ReadIdpAsync(ReadIdpParam param, CancellationToken cancellationToken);
    Task<Idp> UpdateIdpAsync(UpdateIdpParam idp, CancellationToken cancellationToken);
    Task DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken);
}
