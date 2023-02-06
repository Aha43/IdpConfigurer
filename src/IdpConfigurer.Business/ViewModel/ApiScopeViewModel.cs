using IdpConfigurer.Domain;

namespace IdpConfigurer.Business.ViewModel;

public record class ApiScopeViewModel
{
    public bool Selected { get; set; }
    public required ApiScope ApiScope { get; init; }
    public string Title => ApiScope.Title();
}
