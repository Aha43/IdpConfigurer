using IdentityModelManager.Domain;
using IdentityModelManager.Domain.Param.Client;
using IdentityModelManager.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModelManager.Business.ViewController
{
    public class ClientViewController
    {
        private readonly IClientApi _clientApi;

        public Client? Client { get; private set; }

        public ClientViewController(IClientApi clientApi) => _clientApi = clientApi;

        public async Task LoadAsync(string idpName, string clientId) => await LoadAsync(idpName, clientId, default).ConfigureAwait(false);
        public async Task LoadAsync(string idpName, string clientId, CancellationToken cancellationToken)
        {
            var param = new ReadClientParam { IdpName = idpName, ClientId = clientId };
            Client = await _clientApi.ReadClientAsync(param, cancellationToken).ConfigureAwait(false);
        }

    }

}
