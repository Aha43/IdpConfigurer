namespace IdentityModelManager.Domain
{
    public record class Client
    {
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();
    }
}
