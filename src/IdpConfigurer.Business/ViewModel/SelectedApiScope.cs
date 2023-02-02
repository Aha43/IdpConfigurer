using IdentityModelManager.Domain;
using IdpConfigurer.Business;

namespace IdpConfigurer.Business.ViewModel;

public record class SelectedApiScope
{
    public bool Selected { get; set; }
    public required ApiScope ApiScope { get; init; }
    public string Title => ApiScope.Title();
}
