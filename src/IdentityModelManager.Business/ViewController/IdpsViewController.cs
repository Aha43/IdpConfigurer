using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Idp;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Business.ViewController
{
    public class IdpsViewController
    {
        private readonly IIdpApi _idpApi;

        public IEnumerable<Idp> Idps { get; private set; } = Enumerable.Empty<Idp>();

        public IdpsViewController(IIdpApi idpApi) => _idpApi = idpApi;

        public async Task LoadAsync() => await LoadAsync(default).ConfigureAwait(false);
        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            var param = new GetIdpsParam();
            Idps = await _idpApi.ReadIdpsAsync(param, cancellationToken).ConfigureAwait(false);
        }

        public async Task CreateIdpAsync(string name, string uri) => await CreateIdpAsync(name, uri, default).ConfigureAwait(false);
        public async Task CreateIdpAsync(string name, string uri, CancellationToken cancellationToken)
        {
            var param = new CreateIdpParam { Name = name, Uri = uri };
            Idps = await _idpApi.CreateIdpAsync(param, cancellationToken).ConfigureAwait(false);
        }

    }

}
