using IdentityModelManager.Business.ViewController;
using IdentityModelManager.Infrastructure.Memory;
using IdentityModelManager.Specification;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityModelManager.Business
{
    public static class Services
    {
        public static IServiceCollection AddIdentityModelManagerInMemoryServices(this IServiceCollection services)
        {
            return services
                .AddApis()
                .AddIdentityModelManagerViewControllers();
        }

        private static IServiceCollection AddApis(this IServiceCollection services) 
        {
            return services
                .AddSingleton<IIdpApi, InMemoryIdpRepository>()
                .AddSingleton<IClientApi, InMemoryClientRepository>();
        }

        private static IServiceCollection AddIdentityModelManagerViewControllers(this IServiceCollection services) 
        {
            return services
                .AddSingleton<IdpViewController>()
                .AddSingleton<IdpsViewController>();
        }

    }

}
