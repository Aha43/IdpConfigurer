namespace IdpConfigurer.Domain
{
    public record class Client
    {
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }

        public bool AllowOfflineAccess { get; set; } = false;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;

        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();
        public ICollection<string> AllowedScopes { get; set; } = new HashSet<string>();
    }
}
