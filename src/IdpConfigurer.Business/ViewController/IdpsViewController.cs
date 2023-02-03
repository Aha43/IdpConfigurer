using IdentityModelManager.Specification;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;

namespace IdpConfigurer.Business.ViewController
{
    public class IdpsViewController
    {
        private readonly IIdpApi _idpApi;

        private readonly List<Idp> _idps = new();

        public IEnumerable<Idp> Idps => _idps.AsEnumerable();

        public IdpsViewController(IIdpApi idpApi) => _idpApi = idpApi;

        public async Task LoadAsync() => await LoadAsync(default).ConfigureAwait(false);
        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            var idps = await _idpApi.ReadIdpsAsync(cancellationToken).ConfigureAwait(false);
            _idps.Clear();
            _idps.AddRange(idps);
        }

        public string? NameToNewIdp { get; set; } = string.Empty;
        public string? UriToNewIdp { set; get; } = string.Empty;

        public async Task CreateIdpAsync() => await CreateIdpAsync(default).ConfigureAwait(false);
        public async Task CreateIdpAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(NameToNewIdp) && !string.IsNullOrWhiteSpace(UriToNewIdp))
            {
                var param = new CreateIdpParam { Name = NameToNewIdp.Trim(), Uri = UriToNewIdp.Trim() };
                var created = await _idpApi.CreateIdpAsync(param, cancellationToken).ConfigureAwait(false);
                _idps.Add(created);
            }

            NameToNewIdp = string.Empty;
            UriToNewIdp = string.Empty;
        }

    }

}
