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
            return services.AddSingleton<IIdpApi, InMemoryIdpRepository>().
                AddIdentityModelManagerViewControllers();
        }

        private static IServiceCollection AddIdentityModelManagerViewControllers(this IServiceCollection services) 
        {
            return services.AddSingleton<IdpsViewController>();
        }

    }

}
