using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification.Api;
using Microsoft.AspNetCore.Mvc;

namespace IdpConfigurer.Application.WebApi;

public static class MapApiExtensions
{
    public static WebApplication MapIdpProviderApis(this WebApplication app)
    {
        app.MapGroup("/idp").MapIdpApi()
            .MapGroup("/client").MapClientApi()
            .MapGroup("/apiscope").MapApiScopeApi();
        return app;
    }

    public static RouteGroupBuilder MapIdpApi(this RouteGroupBuilder g)
    {
        g.MapGet("/", async (IIdpApi api, CancellationToken ct) => 
            await api.ReadIdpsAsync(ct));

        g.MapGet("/{name}", async (IIdpApi api, [AsParameters] ReadIdpParam p, CancellationToken ct) => 
            await api.ReadIdpAsync(p, ct));

        g.MapPost("/", async (IIdpApi api, [FromBody] CreateIdpParam p, CancellationToken ct) => 
            await api.CreateIdpAsync(p, ct));

        g.MapPut("/", async (IIdpApi api, [FromBody] UpdateIdpParam p, CancellationToken ct) => await api.UpdateIdpAsync(p, ct));

        g.MapDelete("/{name}", async (IIdpApi api, [AsParameters] DeleteIdpParam p, CancellationToken ct) => await api.DeleteIdpAsync(p, ct));

        return g;
    }

    public static RouteGroupBuilder MapClientApi(this RouteGroupBuilder g) 
    {
        g.MapGet("/{idpname}", async (IClientApi api, [AsParameters] ReadClientsParam p, CancellationToken ct) =>
            await api.ReadClientsAsync(p, ct));

        g.MapGet("/{idpname}/{clientid}", async (IClientApi api, [AsParameters] ReadClientParam p, CancellationToken ct) => 
            await api.ReadClientAsync(p, ct));

        g.MapPost("/", async (IClientApi api, [FromBody] CreateClientParam p, CancellationToken ct) => 
            await api.CreateClientAsync(p, ct));

        g.MapPut("/", async (IClientApi api, [FromBody] UpdateClientParam p, CancellationToken ct) => 
            await api.UpdateClientAsync(p, ct));

        g.MapDelete("/{idpname}/{clientid}", async (IClientApi api, [AsParameters] DeleteClientParam p, CancellationToken ct) => 
            await api.DeleteClientAsync(p, ct));

        return g;
    }

    public static RouteGroupBuilder MapApiScopeApi(this RouteGroupBuilder g) 
    {
        g.MapGet("/{idpname}", async (IApiScopeApi api, [AsParameters] ReadApiScopesParam p, CancellationToken ct) =>
            await api.ReadApiScopesAsync(p, ct));

        g.MapGet("/{idpname}/{name}", async (IApiScopeApi api, [AsParameters] ReadApiScopeParam p, CancellationToken ct) =>
            await api.ReadApiScopeAsync(p, ct));

        g.MapPost("/", async (IApiScopeApi api, [FromBody] CreateApiScopeParam p, CancellationToken ct) =>
            await api.CreateApiScopeAsync(p, ct));

        g.MapPut("/", async (IApiScopeApi api, [FromBody] UpdateApiScopeParam p, CancellationToken ct) =>
            await api.UpdateApiScopeAsync(p, ct));

        g.MapDelete("/{idpname}/{name}", async (IApiScopeApi api, [AsParameters] DeleteApiScopeParam p, CancellationToken ct) =>
            await api.DeleteApiScopeAsync(p, ct));

        return g;
    }

}
