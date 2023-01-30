using IdentityModelManager.Business.ViewController;
using IdentityModelManager.Infrastructure.Memory;
using IdentityModelManager.Specification;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
