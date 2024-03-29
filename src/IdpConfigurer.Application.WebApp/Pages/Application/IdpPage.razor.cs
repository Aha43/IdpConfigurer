using IdpConfigurer.Business.ViewController;
using Microsoft.AspNetCore.Components;

namespace IdpConfigurer.Application.WebApp.Pages.Application;

public partial class IdpPage
{
    [Parameter] public string? IdpName { get; set; }

#nullable disable
    [Inject] private IdpViewController ViewController { get; set; }
#nullable enable

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrWhiteSpace(IdpName))
        {
            await ViewController.LoadAsync(IdpName);
        }
    }

}
