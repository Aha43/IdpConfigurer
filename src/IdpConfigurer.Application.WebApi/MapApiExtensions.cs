using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Application.WebApi;

public static class MapApiExtensions
{
    public static RouteGroupBuilder MapIdpApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IIdpApi api) => await api.ReadIdpsAsync(default));

        return group;
    }
}
