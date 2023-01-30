﻿using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Idp;

namespace IdentityModelManager.Specification;

public interface IIdpApi
{
    Task<IEnumerable<Idp>> CreateIdpAsync(CreateIdpParam param, CancellationToken cancellationToken);
    Task<IEnumerable<Idp>> ReadIdpsAsync(GetIdpsParam param, CancellationToken cancellationToken);
    Task<Idp> UpdateIdpAsync(Idp idp, CancellationToken cancellationToken);
    Task<bool> DeleteIdpAsync(DeleteIdpParam param, CancellationToken cancellationToken);
}
