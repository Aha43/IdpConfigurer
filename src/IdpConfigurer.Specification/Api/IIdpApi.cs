using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;

namespace IdpConfigurer.Specification.Api;

public interface IIdpApi
{
    Task<Idp> CreateIdpAsync(CreateIdpParam p, CancellationToken ct);
    Task<IEnumerable<Idp>> ReadIdpsAsync(CancellationToken ct);
    Task<IEnumerable<Idp>> ReadIdpAsync(ReadIdpParam p, CancellationToken ct);
    Task<Idp> UpdateIdpAsync(UpdateIdpParam p, CancellationToken ct);
    Task DeleteIdpAsync(DeleteIdpParam p, CancellationToken ct);
}
