using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Idp;
using IdentityModelManager.Specification;

namespace IdentityModelManager.Business.ViewController
{
    public class IdpViewController
    {
        private readonly IIdpApi _api;

        public Idp? Idp { get; private set; }

        public IdpViewController(IIdpApi api) => _api = api;

        public async Task LoadAsync(string name) => await LoadAsync(name, default).ConfigureAwait(false);
        public async Task LoadAsync(string name, CancellationToken cancellationToken = default)
        {
            var param = new ReadIdpParam { Name = name };
            Idp = await _api.ReadIdpAsync(param, cancellationToken).ConfigureAwait(false);
        }

    }

}
