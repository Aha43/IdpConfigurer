using IdpConfigurer.Specification.Api;
using IdpConfigurer.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdpConfigurer.Infrastructure.WebApi.Clients
{
    public static class Services
    {
        public static IServiceCollection AddIdpConfigurerWebApiClients(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApis()
                .ConfigureHttpClients(configuration);
        }

        private static IServiceCollection AddApis(this IServiceCollection services)
        {
            return services.AddSingleton<IIdpApi, IdpWebApiClient>()
                .AddSingleton<IClientApi, ClientWebApiClient>()
                .AddSingleton<IApiScopeApi, ApiScopeWebApiClient>();
        }

        private static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration) 
        {
            var config = configuration.GetRequiredSectionAs<IdpConfigurerWebApiClientsConfiguration>();
            var baseUri = new Uri(config.BaseUri);

            services.AddHttpClient(nameof(IdpWebApiClient), o => 
            {
                o.BaseAddress = new Uri(baseUri, "idp");
            });

            services.AddHttpClient(nameof(ClientWebApiClient), o =>
            {
                o.BaseAddress = new Uri(baseUri, "client");
            });

            services.AddHttpClient(nameof(ApiScopeWebApiClient), o =>
            {
                o.BaseAddress = new Uri(baseUri, "apiscope");
            });

            return services;
        }
    }

    public class IdpConfigurerWebApiClientsConfiguration
    {
        public required string BaseUri { get; set; }
    }

}
