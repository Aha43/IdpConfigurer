using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification.Api;
using Microsoft.AspNetCore.Mvc;

namespace IdpConfigurer.Application.WebApi;

public static class MapApiExtensions
{
    public static RouteGroupBuilder MapIdpApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IIdpApi api, CancellationToken cancellation) => 
            await api.ReadIdpsAsync(cancellation));

        group.MapGet("/{name}", async (IIdpApi api, [AsParameters] ReadIdpParam p, CancellationToken cancellationToken) => 
            await api.ReadIdpAsync(p, cancellationToken));

        group.MapPost("/create", async (IIdpApi api, [FromBody] CreateIdpParam p, CancellationToken cancellationToken) => 
            await api.CreateIdpAsync(p, cancellationToken));

        group.MapPut("/update", async (IIdpApi api, [FromBody] UpdateIdpParam p, CancellationToken cancellationToken) =>
            await api.UpdateIdpAsync(p, cancellationToken));

        group.MapDelete("/delete/{name}", async (IIdpApi api, [AsParameters] DeleteIdpParam p, CancellationToken cancellationToken) =>
            await api.DeleteIdpAsync(p, cancellationToken));

        return group;
    }
}
