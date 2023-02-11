using IdpConfigurer.Domain;

namespace IdpConfigurer.Business.ViewController
{
    public partial class ClientViewController
    {
        public bool Hybrid { get; set; }
        public bool ResourceOwnerPassword { get; set; }
        public bool Implicit { get; set; }
        public bool AuthorizationCode { get; set; }
        public bool ClientCredentials { get; set; }

        public async Task SaveGrantsFlows() => await SaveGrantsFlows(default);
        public async Task SaveGrantsFlows(CancellationToken cancellationToken)
        {
            if (Client == null) return;

            Client.AllowedGrantTypes.Clear();

            if (Hybrid) Client.AllowedGrantTypes.Add(GrantType.Hybrid);
            if (ResourceOwnerPassword) Client.AllowedGrantTypes.Add(GrantType.ResourceOwnerPassword);
            if (Implicit) Client.AllowedGrantTypes.Add(GrantType.Implicit);
            if (AuthorizationCode) Client.AllowedGrantTypes.Add(GrantType.AuthorizationCode);
            if (ClientCredentials) Client.AllowedGrantTypes.Add(GrantType.ClientCredentials);

            await UpdateClient(cancellationToken);
        }

        private void SetGrantsFlows(Client client)
        {
            Hybrid = false;
            Implicit = false;
            AuthorizationCode = false;
            ResourceOwnerPassword = false;
            ClientCredentials = false;

            foreach (var g in client.AllowedGrantTypes)
            {
                switch (g)
                {
                    case GrantType.Hybrid: Hybrid = true; break;
                    case GrantType.ResourceOwnerPassword: ResourceOwnerPassword = true; break;
                    case GrantType.ClientCredentials: ClientCredentials = true; break;
                    case GrantType.Implicit: Implicit = true; break;
                    case GrantType.AuthorizationCode: AuthorizationCode = true; break;
                }
            }
        }

    }

}
