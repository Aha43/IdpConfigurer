﻿using IdpConfigurer.Business.ViewModel;
using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.ApiScope;
using IdpConfigurer.Domain.Param.Client;
using IdpConfigurer.Domain.Param.Idp;
using IdpConfigurer.Specification.Api;

namespace IdpConfigurer.Business.ViewController
{
    public class IdpViewController
    {
        private readonly IIdpApi _idpApi;
        private readonly IClientApi _clientApi;
        private readonly IApiScopeApi _apiScopeApi;

        private readonly IdpCustomDataDefinition _idpCustomDataDefinition;

        public Idp? Idp { get; private set; }

        private readonly List<Client> _clients = new();
        public IEnumerable<Client> Clients => _clients.AsEnumerable();

        private readonly List<ApiScope> _apiScopes = new();
        public IEnumerable<ApiScope> ApiScopes => _apiScopes.AsEnumerable();

        public IdpCustomFieldViewModel[] CustomFields { get; private set; } = Array.Empty<IdpCustomFieldViewModel>();

        public IdpViewController(
            IIdpApi idpApi,
            IClientApi clientApi,
            IApiScopeApi apiScopeApi,
            IdpCustomDataDefinition idpCustomDataDefinition)
        {
            _idpApi = idpApi;
            _clientApi = clientApi;
            _apiScopeApi = apiScopeApi;
            _idpCustomDataDefinition = idpCustomDataDefinition;
        }

        public async Task LoadAsync(string name) => await LoadAsync(name, default).ConfigureAwait(false);
        public async Task LoadAsync(string name, CancellationToken cancellationToken = default)
        {   
            Idp = null;

            var readIdParam = new ReadIdpParam { Name = name };
            var idps = await _idpApi.ReadIdpAsync(readIdParam, cancellationToken).ConfigureAwait(false);
            var idp = idps.FirstOrDefault();

            if (idp != null) 
            {
                _clients.Clear();
                var readClientsParam = new ReadClientsParam { IdpName = name };
                var clients = await _clientApi.ReadClientsAsync(readClientsParam, cancellationToken).ConfigureAwait(false);
                _clients.AddRange(clients);

                _apiScopes.Clear();
                var readApiScopesParam = new ReadApiScopesParam { IdpName = name };
                var apis = await _apiScopeApi.ReadApiScopesAsync(readApiScopesParam, cancellationToken).ConfigureAwait(false);
                _apiScopes.AddRange(apis);

                if (idp.Data.CustomData.Update(_idpCustomDataDefinition))
                {
                    await Update(cancellationToken).ConfigureAwait(false);
                }
                CustomFields = idp.Data.CustomData.CustomFields.Select(e => new IdpCustomFieldViewModel(e)).ToArray();

                Idp = idp;
            }
        }

        public bool EditIdpName { get; set; }
        public string? NewIdpName { get; set; }

        public async Task UpdateIdpName() => await UpdateIdpName(default);
        public async Task UpdateIdpName(CancellationToken cancellationToken)
        {
            EditIdpName = false;

            if (string.IsNullOrWhiteSpace(NewIdpName)) return;

            await Update(NewIdpName.Trim(), cancellationToken);

            NewIdpName = null;
        }

        public string? NewClientName { get; set; }
        public string? NewClientId { get; set; }

        public async Task CreateClientAsync() => await CreateClientAsync(default).ConfigureAwait(true);
        public async Task CreateClientAsync(CancellationToken cancellationToken)
        {
            if (Idp != null && !string.IsNullOrEmpty(NewClientId))
            {
                if (string.IsNullOrWhiteSpace(NewClientName)) NewClientName = NewClientId;
                var param = new CreateClientParam { ClientName = NewClientName, ClientId = NewClientId, IdpName = Idp.Name };
                var client = await _clientApi.CreateClientAsync(param, cancellationToken).ConfigureAwait(true);
                _clients.Add(client);
            }

            NewClientId = null;
            NewClientName = null;
        }

        public string? NewApiScopeName { get; set; }
        public string? NewApiScopeDisplayName { get; set; }

        public async Task CreateApiScope() => await CreateApiScope(default).ConfigureAwait(true);
        public async Task CreateApiScope(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(NewApiScopeName) && Idp != null)
            {
                var displayName = string.IsNullOrWhiteSpace(NewApiScopeDisplayName) ? NewApiScopeName : NewApiScopeDisplayName;
                var param = new CreateApiScopeParam { Name = NewApiScopeName, DisplayName = displayName, IdpName = Idp.Name };
                var scope = await _apiScopeApi.CreateApiScopeAsync(param, cancellationToken).ConfigureAwait(true);
                _apiScopes.Add(scope);
            }

            NewApiScopeName = null;
            NewApiScopeDisplayName = null;
        }

        public async Task Update() => await Update(default).ConfigureAwait(true);

        public async Task Update(CancellationToken cancellationToken)
        {
            if (Idp == null) return;

            var updateParam = new UpdateIdpParam { Idp = Idp };
            var updated = await _idpApi.UpdateIdpAsync(updateParam, cancellationToken);
            Idp = updated;
        }

        private async Task Update(string? newName = null, CancellationToken cancellationToken = default)
        {
            if (Idp == null) return;

            var updateParam = new UpdateIdpParam { IdpName = newName, Idp = Idp };
            var updated = await _idpApi.UpdateIdpAsync(updateParam, cancellationToken);
            Idp = updated;
        }

    }

}
