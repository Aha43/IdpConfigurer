namespace IdpConfigurer.Application.WebApp.Configuration
{
    public record class Credentials
    {
        public string? AuthorityUri { get; set; }
        public string? ClientId { get; set; }
        public string? SharedSecret { get; set; }
    }
}
