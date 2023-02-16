using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Business.ViewController
{
    public class IdpsViewController
    {
        private readonly IIdpApi _idpApi;

        private readonly IdpCustomDataDefinition _idpCustomDataDefinition;

        private readonly List<Idp> _idps = new();

        public IEnumerable<Idp> Idps => _idps.AsEnumerable();

        public IdpsViewController(IIdpApi idpApi, IdpCustomDataDefinition idpCustomDataDefinition)
        {
            _idpApi = idpApi;
            _idpCustomDataDefinition = idpCustomDataDefinition;
        }

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
                var readParam = new CreateIdpParam { Name = NameToNewIdp.Trim(), Uri = UriToNewIdp.Trim() };
                var created = await _idpApi.CreateIdpAsync(readParam, cancellationToken).ConfigureAwait(false);

                created.Data.CustomData = _idpCustomDataDefinition.Create();

                var updateParam = new UpdateIdpParam { IdpName = created.Name, Idp = created };
                var updated = await _idpApi.UpdateIdpAsync(updateParam, cancellationToken).ConfigureAwait(false);

                _idps.Add(updated);
            }

            NameToNewIdp = string.Empty;
            UriToNewIdp = string.Empty;
        }

    }

}
