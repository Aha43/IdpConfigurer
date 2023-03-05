using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Infrastructure.WebApi.Clients;
using IdpConfigurer.Specification.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest.IdpConfigurer.Infrastructure.WebApi.Clients
{
    public class CreudTests
    {
        private static IServiceProvider Configure()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            var services = new ServiceCollection()
                .AddIdpConfigurerWebApiClients(configuration);
            return services.BuildServiceProvider();
        }

        [Fact]
        public async Task IdpCrudShouldWorkAsync()
        {
            var services = Configure();

            var api = services.GetRequiredService<IIdpApi>();

            await TestIdpCreateAsync(api, "test1");
            await TestIdpCreateAsync(api, "test2");

            await TestIdpReadAsync(api, "test1");
            await TestIdpReadAsync(api, "test2");
        }

        private static async Task TestIdpCreateAsync(IIdpApi api, string name)
        {
            var uri = $"http://{name}";

            var p = new CreateIdpParam { Name = name, Uri = uri };

            var idp = await api.CreateIdpAsync(p, default).ConfigureAwait(false);

            Assert.NotNull(idp);
            Assert.Equal(name, idp.Name);
            Assert.Equal(uri, idp.Uri);
        }

        private static async Task TestIdpReadAsync(IIdpApi api, string name)
        {
            var p = new ReadIdpParam { Name = name };

            var idps = await api.ReadIdpAsync(p, default).ConfigureAwait(false);

            var uri = $"http://{name}";

            Assert.NotNull(idps);

            var idp = idps.FirstOrDefault();

            Assert.NotNull(idp);
            Assert.Equal(name, idp.Name);
            Assert.Equal(uri, idp.Uri);
        }

    }

}
