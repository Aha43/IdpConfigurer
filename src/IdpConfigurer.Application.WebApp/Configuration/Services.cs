using IdpConfigurer.Business;

namespace IdpConfigurer.Application.WebApp.Configuration
{
    public static class Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSecurity(configuration)
                .AddIdpConfigurerServices(configuration);
        }

        private static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // ToDo
            return services;
        }
    }
}
