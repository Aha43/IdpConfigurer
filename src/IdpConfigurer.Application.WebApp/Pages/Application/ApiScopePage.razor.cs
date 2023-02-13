using IdpConfigurer.Business.ViewController;
using Microsoft.AspNetCore.Components;

namespace IdpConfigurer.Application.WebApp.Pages.Application
{
    public partial class ApiScopePage
    {
        [Parameter] public string? IdpName { get; set; }
        [Parameter] public string? Name { get; set; }

#nullable disable
        [Inject] private ApiScopeViewController ViewController { get; set; }
#nullable enable

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrWhiteSpace(IdpName) && !string.IsNullOrWhiteSpace(Name))
            {
                await ViewController.LoadAsync(IdpName, Name).ConfigureAwait(true);
            }
        }

    }

}
