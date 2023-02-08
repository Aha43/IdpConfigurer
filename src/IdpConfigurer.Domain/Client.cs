namespace IdpConfigurer.Domain
{
    public record class Client
    {
        public required string ClientId { get; set; }
        public required string ClientName { get; set; }

        public ICollection<ClientSecret> ClientSecrets = new List<ClientSecret>();

        public bool AllowOfflineAccess { get; set; } = false;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;

        public ICollection<string> AllowedGrantTypes = new HashSet<string>();

        public ICollection<string> RedirectUris = new HashSet<string>();

        public ICollection<string> PostLogoutRedirectUris = new HashSet<string>();

        public ICollection<string> AllowedScopes = new HashSet<string>();
    }
}
