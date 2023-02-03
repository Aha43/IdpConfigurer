using IdpConfigurer.Domain;

namespace IdpConfigurer.Business;

public static class DtoViewExtensions
{
    public static string PageUri(this Idp idp) => $"/Idp/{idp.Name}";

    public static string PageUri(this Client client, Idp idp) => $"/Idp/{idp.Name}/Client/{client.ClientId}";

    public static string Title(this ApiScope apiScope) => string.IsNullOrWhiteSpace(apiScope.DisplayName) ? apiScope.Name : apiScope.DisplayName;
    public static string PageUri(this ApiScope apiScope, Idp idp) => $"/Idp/{idp.Name}/ApiScope/{apiScope.Name}";
}
